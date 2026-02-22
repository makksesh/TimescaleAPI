namespace Application.UseCases.UploadCsv;

public class UploadCsvRequest
{
    // Имя файла — для upsert логики и записи в БД
    public required string FileName { get; init; }

    // Stream — универсальный тип, не привязан к HTTP
    // Можно передать из теста, из файловой системы, из S3 — без изменений UseCase
    public required Stream FileStream { get; init; }
}