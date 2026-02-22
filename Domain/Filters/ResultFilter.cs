namespace Domain.Filters;


/// <summary>
/// Объект-параметр запроса
/// </summary>
public class ResultFilter
{
    public string? FileName { get; init; }
    public DateTime? FirstOperationStartFrom { get; init; }
    public DateTime? FirstOperationStartTo { get; init; }
    public double? AvgValueFrom { get; init; }
    public double? AvgValueTo { get; init; }
    public double? AvgExecutionTimeFrom { get; init; }
    public double? AvgExecutionTimeTo { get; init; }
}