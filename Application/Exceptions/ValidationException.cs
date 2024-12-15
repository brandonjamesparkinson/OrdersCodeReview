namespace Application.Exceptions;

public class ValidationException : Exception
{

    public ValidationException(string message,
                               IDictionary<string, string> errors)
        : base(message)
    {
        Errors = errors;
    }

    // Errors is currently settable, may want it read only?
    public IDictionary<string, string> Errors { get; set; }
}