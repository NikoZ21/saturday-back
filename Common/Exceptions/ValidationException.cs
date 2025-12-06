namespace Saturday_Back.Common.Exceptions
{
    /// <summary>
    /// Exception thrown when model or DTO validation fails.
    /// Typically used for invalid request data.
    /// </summary>
    public class ValidationException : Exception
    {
        public int StatusCode { get; }
        public string ValidationLayer { get; } = "Model";

        public ValidationException(string message, int statusCode = 400) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}

