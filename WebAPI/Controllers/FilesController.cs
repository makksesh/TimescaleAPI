using Application.UseCases.GetLast10Values;
using Application.UseCases.GetResults;
using Application.UseCases.UploadCsv;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Requests;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly UploadCsvUseCase _uploadUseCase;
    private readonly GetResultsUseCase _getResultsUseCase;
    private readonly GetLast10ValuesUseCase _getLast10UseCase;

    public FilesController(
        UploadCsvUseCase uploadUseCase,
        GetResultsUseCase getResultsUseCase,
        GetLast10ValuesUseCase getLast10UseCase)
    {
        _uploadUseCase = uploadUseCase;
        _getResultsUseCase = getResultsUseCase;
        _getLast10UseCase = getLast10UseCase;
    }

    /// <summary>Загрузить CSV файл и сохранить данные</summary>
    [HttpPost("upload")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upload(
        IFormFile file, CancellationToken ct)
    {
        var request = new UploadCsvRequest
        {
            FileName = file.FileName,
            FileStream = file.OpenReadStream()
        };

        await _uploadUseCase.ExecuteAsync(request, ct);
        return Ok();
    }

    /// <summary>Получить результаты по фильтрам</summary>
    [HttpGet("results")]
    [ProducesResponseType(typeof(IEnumerable<ResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetResults(
        [FromQuery] GetResultsHttpRequest httpRequest, CancellationToken ct)
    {
        // Маппинг HTTP query params → Application Request
        var request = new GetResultsRequest
        {
            FileName = httpRequest.FileName,
            FirstOperationStartFrom = httpRequest.FirstOperationStartFrom,
            FirstOperationStartTo = httpRequest.FirstOperationStartTo,
            AvgValueFrom = httpRequest.AvgValueFrom,
            AvgValueTo = httpRequest.AvgValueTo,
            AvgExecutionTimeFrom = httpRequest.AvgExecutionTimeFrom,
            AvgExecutionTimeTo = httpRequest.AvgExecutionTimeTo
        };

        var results = await _getResultsUseCase.ExecuteAsync(request, ct);
        return Ok(results);
    }

    /// <summary>Получить последние 10 значений по имени файла</summary>
    [HttpGet("{fileName}/values/last10")]
    [ProducesResponseType(typeof(IEnumerable<ValueEntryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLast10Values(
        string fileName, CancellationToken ct)
    {
        var request = new GetLast10ValuesRequest { FileName = fileName };
        var values = await _getLast10UseCase.ExecuteAsync(request, ct);
        return Ok(values);
    }
}