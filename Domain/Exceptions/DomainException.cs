namespace Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}

public class ValidationException : DomainException
{
    public IReadOnlyList<string> Errors { get; }
    
    public ValidationException(IReadOnlyList<string> errors)
        : base("Файл не прошёл валидацию")
    {
        Errors = errors;
    }
}