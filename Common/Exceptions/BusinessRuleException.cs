namespace Saturday_Back.Common.Exceptions
{
    /// <summary>
    /// Exception thrown when business rules are violated.
    /// Typically used for duplicate records, invalid business logic, etc.
    /// </summary>
    public class BusinessRuleException : Exception
    {
        public int StatusCode { get; }
        public string Layer { get; } = "Business";

        public BusinessRuleException(string message, int statusCode = 409) : base(message)
        {
            StatusCode = statusCode;
        }

    }
}

