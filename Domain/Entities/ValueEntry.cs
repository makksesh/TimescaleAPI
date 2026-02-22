namespace Domain.Entities;


/// <summary>
/// Модель для данных из csv
/// </summary>
public class ValueEntry
{
    public long Id { get; private set; }
    public string FileName { get; private set; }
    public DateTime Date { get; private set; }
    public double ExecutionTime { get; private set; }
    public double Value { get; private set; }
    
    
    private ValueEntry() { }

    /// <summary>
    /// Фабричный метод для данных из csv
    /// </summary>
    /// <param name="fileName">Имя файла</param>
    /// <param name="date">Время начала ГГГГ-ММ-ДДTчч-мм-сс.ммммZ</param>
    /// <param name="executionTime">Время выполнения в секундах</param>
    /// <param name="value">Показатель в виде числа с плавающей запятой</param>
    public static ValueEntry Create(string fileName, DateTime date,
        double executionTime, double value)
    {
        return new ValueEntry
        {
            FileName = fileName,
            Date = date,
            ExecutionTime = executionTime,
            Value = value
        };
    }
}
