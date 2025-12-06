namespace Saturday_Back.Common.Exceptions
{
    /// <summary>
    /// Exception thrown when data access operations fail.
    /// Used for entity not found, database constraint violations, etc.
    /// </summary>
    public class DataAccessException : Exception
    {
        public int StatusCode { get; }
        public string Layer { get; } = "Database";

        public DataAccessException(string message, int statusCode = 500) : base(message)
        {
            StatusCode = statusCode;
        }

    }
}

