using Domain.Filters;
using Domain.Interfaceses;

namespace Application.UseCases.GetResults;

public class GetResultsUseCase
{
    private readonly IResultRepository _resultRepo;

    public GetResultsUseCase(IResultRepository resultRepo)
    {
        _resultRepo = resultRepo;
    }

    public async Task<IEnumerable<ResultDto>> ExecuteAsync(
        GetResultsRequest request, CancellationToken ct = default)
    {
        var filter = new ResultFilter
        {
            FileName = request.FileName,
            FirstOperationStartFrom = request.FirstOperationStartFrom,
            FirstOperationStartTo = request.FirstOperationStartTo,
            AvgValueFrom = request.AvgValueFrom,
            AvgValueTo = request.AvgValueTo,
            AvgExecutionTimeFrom = request.AvgExecutionTimeFrom,
            AvgExecutionTimeTo = request.AvgExecutionTimeTo
        };

        var results = await _resultRepo.GetFilteredAsync(filter, ct);
        
        return results.Select(r => new ResultDto
        {
            FileName = r.FileName,
            DeltaSeconds = r.DeltaSeconds,
            FirstOperationStart = r.FirstOperationStart,
            AvgExecutionTime = r.AvgExecutionTime,
            AvgValue = r.AvgValue,
            MedianValue = r.MedianValue,
            MaxValue = r.MaxValue,
            MinValue = r.MinValue
        });
    }
}
