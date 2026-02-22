namespace Domain.Entities;

public class Result
{
    public long Id { get; private set; }
    public string FileName { get; private set; }
    public double DeltaSeconds { get; private set; }
    public DateTime FirstOperationStart { get; private set; }
    public double AvgExecutionTime { get; private set; }
    public double AvgValue { get; private set; }
    public double MedianValue { get; private set; }
    public double MaxValue { get; private set; }
    public double MinValue { get; private set; }

    private Result() { }

    /// <summary>
    /// Фабричный метод для интегральных результатов из данных csv
    /// </summary>
    /// <param name="fileName">имя файла</param>
    /// <param name="deltaSeconds">дельта времени Date в секундах (максимальное Date – минимальное Date)</param>
    /// <param name="firstOperationStart">минимальное дата и время, как момент запуска первой операции</param>
    /// <param name="avgExecutionTime">среднее время выполнения</param>
    /// <param name="avgValue">среднее значение по показателям</param>
    /// <param name="medianValue">медина по показателям</param>
    /// <param name="maxValue">максимальное значение показателя</param>
    /// <param name="minValue">минимальное значение показателя</param>
    /// <returns></returns>
    public static Result Create(string fileName, double deltaSeconds,
        DateTime firstOperationStart, double avgExecutionTime,
        double avgValue, double medianValue,
        double maxValue, double minValue)
    {
        return new Result
        {
            FileName = fileName,
            DeltaSeconds = deltaSeconds,
            FirstOperationStart = firstOperationStart,
            AvgExecutionTime = avgExecutionTime,
            AvgValue = avgValue,
            MedianValue = medianValue,
            MaxValue = maxValue,
            MinValue = minValue
        };
    }
}