namespace Application.UseCases.UploadCsv;

public class CsvRowDto
{
    public DateTime Date { get; set; }
    public double ExecutionTime { get; set; }
    public double Value { get; set; }
}