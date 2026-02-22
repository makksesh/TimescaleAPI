namespace Application.UseCases.GetLast10Values;

public class ValueEntryDto
{
    public DateTime Date { get; init; }
    public double ExecutionTime { get; init; }
    public double Value { get; init; }
}