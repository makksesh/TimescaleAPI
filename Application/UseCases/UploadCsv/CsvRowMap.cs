using CsvHelper.Configuration;

namespace Application.UseCases.UploadCsv;

public class CsvRowMap : ClassMap<CsvRowDto>
{
    public CsvRowMap()
    {
        Map(m => m.Date)
            .TypeConverter<CsvDateTimeConverter>(); // наш кастомный конвертер

        Map(m => m.ExecutionTime);
        Map(m => m.Value);
    }
}