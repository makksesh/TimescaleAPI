using Application.UseCases.GetLast10Values;
using Application.UseCases.GetResults;
using Application.UseCases.UploadCsv;
using Application.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // Use Cases
        services.AddScoped<UploadCsvUseCase>();
        services.AddScoped<GetResultsUseCase>();
        services.AddScoped<GetLast10ValuesUseCase>();

        // FluentValidation
        services.AddScoped<IValidator<CsvRowDto>, CsvRowValidator>();

        return services;
    }
}