using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Requests;

public class GetResultsHttpRequest
{
    [FromQuery(Name = "fileName")]
    public string? FileName { get; set; }

    [FromQuery(Name = "firstOpStartFrom")]
    public DateTime? FirstOperationStartFrom { get; set; }

    [FromQuery(Name = "firstOpStartTo")]
    public DateTime? FirstOperationStartTo { get; set; }

    [FromQuery(Name = "avgValueFrom")]
    public double? AvgValueFrom { get; set; }

    [FromQuery(Name = "avgValueTo")]
    public double? AvgValueTo { get; set; }

    [FromQuery(Name = "avgExecTimeFrom")]
    public double? AvgExecutionTimeFrom { get; set; }

    [FromQuery(Name = "avgExecTimeTo")]
    public double? AvgExecutionTimeTo { get; set; }
}