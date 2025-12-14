using System.Diagnostics;

namespace Saturday_Back.Common.Exceptions
{
    /// <summary>
    /// Exception thrown when business rules are violated.
    /// Typically used for duplicate records, invalid business logic, etc.
    /// </summary>
    public class BusinessRuleException(string message, string userMessage, int statusCode = 409) : Exception(message)
    {
        public int StatusCode { get; } = statusCode;
        public string Layer { get; } = "Business";
        public string UserMessage { get; } = userMessage;


        private static string FormatMessage(string message)
        {
            try
            {
                var stackTrace = new StackTrace(2, false);
                var frame = stackTrace.GetFrame(0);

                if (frame != null)
                {
                    var method = frame.GetMethod();
                    if (method != null && method.DeclaringType != null)
                    {
                        var className = method.DeclaringType.Name;
                        return $"{className}: {message}";
                    }
                }
            }
            catch
            {
                return message;
            }

            return message;
        }

    }
}

