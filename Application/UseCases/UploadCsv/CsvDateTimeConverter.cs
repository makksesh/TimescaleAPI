using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Application.UseCases.UploadCsv;

public class CsvDateTimeConverter : DefaultTypeConverter
{
    private const string Format = "yyyy-MM-ddTHH-mm-ss.fffZ";

    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new CsvHelperException(row.Context, $"Дата не может быть пустой");

        if (DateTime.TryParseExact(
                text,
                Format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                out var result))
        {
            return result;
        }

        throw new CsvHelperException(row.Context,
            $"Неверный формат даты '{text}'. Ожидается: {Format}");
    }
}