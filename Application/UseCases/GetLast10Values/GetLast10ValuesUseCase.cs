using Domain.Interfaceses;

namespace Application.UseCases.GetLast10Values;

public class GetLast10ValuesUseCase
{
    private readonly IValueEntryRepository _valueRepo;

    public GetLast10ValuesUseCase(IValueEntryRepository valueRepo)
    {
        _valueRepo = valueRepo;
    }

    public async Task<IEnumerable<ValueEntryDto>> ExecuteAsync(
        GetLast10ValuesRequest request, CancellationToken ct = default)
    {
        var entries = await _valueRepo.GetLast10ByFileNameAsync(request.FileName, ct);

        return entries.Select(e => new ValueEntryDto
        {
            Date = e.Date,
            ExecutionTime = e.ExecutionTime,
            Value = e.Value
        });
    }
}
