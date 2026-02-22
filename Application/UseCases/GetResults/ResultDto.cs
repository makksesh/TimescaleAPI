namespace Application.UseCases.GetResults;

public class ResultDto
{
    public string FileName { get; init; } = string.Empty;
    public double DeltaSeconds { get; init; }
    public DateTime FirstOperationStart { get; init; }
    public double AvgExecutionTime { get; init; }
    public double AvgValue { get; init; }
    public double MedianValue { get; init; }
    public double MaxValue { get; init; }
    public double MinValue { get; init; }
}