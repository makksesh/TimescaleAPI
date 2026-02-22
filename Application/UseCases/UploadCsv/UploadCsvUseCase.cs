using System.Globalization;
using Application.Interfaces;
using Application.Validation;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Domain.Interfaceses;
using ValidationException = Domain.Exceptions.ValidationException;

namespace Application.UseCases.UploadCsv;

public class UploadCsvUseCase
{
    private readonly IValueEntryRepository _valueRepo;
    private readonly IResultRepository _resultRepo;
    private readonly IUnitOfWork _uow;

    public UploadCsvUseCase(IValueEntryRepository valueRepo,
        IResultRepository resultRepo,
        IUnitOfWork uow)
    {
        _valueRepo = valueRepo;
        _resultRepo = resultRepo;
        _uow = uow;
    }

    public async Task ExecuteAsync(UploadCsvRequest request, CancellationToken ct = default)
    {
        // ШАГ 1 — Парсинг
        var rows = ParseCsv(request.FileStream);

        // ШАГ 2 — Валидация
        Validate(rows);

        // ШАГ 3 — Маппинг в доменные сущности
        var entries = rows
            .Select(r => ValueEntry.Create(request.FileName, r.Date, r.ExecutionTime, r.Value))
            .ToList();

        // ШАГ 4 — Расчёт агрегатов
        var result = CalculateResult(request.FileName, rows);

        // ШАГ 5 — Сохранение в транзакции
        await _uow.ExecuteInTransactionAsync(async () =>
        {
            await _valueRepo.DeleteByFileNameAsync(request.FileName, ct);
            await _valueRepo.AddRangeAsync(entries, ct);
            await _resultRepo.UpsertAsync(result, ct);
        }, ct);
    }
    
    private void Validate(List<CsvRowDto> rows)
    {
        var errors = new List<string>();
        
        if (rows.Count < 1 || rows.Count > 10_000)
            errors.Add($"Недопустимое количество строк: {rows.Count}. Допустимо от 1 до 10 000.");
        
        var validator = new CsvRowValidator();
        for (int i = 0; i < rows.Count; i++)
        {
            var result = validator.Validate(rows[i]);
            if (!result.IsValid)
                errors.AddRange(result.Errors.Select(e => $"Строка {i + 2}: {e.ErrorMessage}"));
        }

        if (errors.Count > 0)
            throw new ValidationException(errors); // Domain exception → откат транзакции
    }
    
    private Result CalculateResult(string fileName, List<CsvRowDto> rows)
    {
        var dates = rows.Select(r => r.Date).ToList();
        var values = rows.Select(r => r.Value).OrderBy(v => v).ToArray();
        var execTimes = rows.Select(r => r.ExecutionTime).ToList();

        var deltaSeconds = (dates.Max() - dates.Min()).TotalSeconds;
        var firstOpStart = dates.Min();
        var avgExecTime = execTimes.Average();
        var avgValue = values.Average();
        var maxValue = values.Max();
        var minValue = values.Min();
        
        var n = values.Length;
        var median = n % 2 == 0
            ? (values[n / 2 - 1] + values[n / 2]) / 2.0
            : values[n / 2];

        return Result.Create(fileName, deltaSeconds, firstOpStart,
            avgExecTime, avgValue, median, maxValue, minValue);
    }
    
    private List<CsvRowDto> ParseCsv(Stream stream)
    {
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = true,
            MissingFieldFound = null // не бросать исключение при пустом поле — обработаем валидацией
        });

        csv.Context.RegisterClassMap<CsvRowMap>();
        
        try
        {
            return csv.GetRecords<CsvRowDto>().ToList();
        }
        catch (CsvHelperException ex)
        {
            throw new ValidationException(new List<string>
            {
                $"Файл невозможно распарсить: {ex.Message}"
            });
        }
    }

}