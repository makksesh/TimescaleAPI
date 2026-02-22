using Application.UseCases.UploadCsv;
using FluentValidation;

namespace Application.Validation;

public class CsvRowValidator : AbstractValidator<CsvRowDto>
{
    public CsvRowValidator()
    {
        RuleFor(r => r.Date)
            .GreaterThanOrEqualTo(new DateTime(2000, 1, 1))
            .WithMessage("Дата не может быть раньше 01.01.2000")
            .LessThanOrEqualTo(_ => DateTime.UtcNow)
            .WithMessage("Дата не может быть позже текущей");

        RuleFor(r => r.ExecutionTime)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Время выполнения не может быть меньше 0");

        RuleFor(r => r.Value)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Показатель не может быть меньше 0");
    }
    
    
}

