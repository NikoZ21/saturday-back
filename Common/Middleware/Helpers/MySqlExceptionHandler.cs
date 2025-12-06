using MySqlConnector;
using System.Text.RegularExpressions;

public static class MySqlExceptionHandler
{

    private const int CANNOT_DELETE_OR_UPDATE = 1451;
    private const int DATA_TOO_LONG_FOR_COLUMN = 1406;
    private const int DUPLICATE_ENTRY = 1062;
    private const int REFERENCED_RECORD_DOES_NOT_EXIST = 1452;
    private const int DATABASE_ERROR = 500;

    public static (int statusCode, string message) HandleMySqlException(MySqlException mysqlEx)
    {
        return mysqlEx.Number switch
        {
            // Duplicate entry (unique constraint violation)
            DUPLICATE_ENTRY => (409, ExtractDuplicateEntryMessage(mysqlEx.Message)),
            // Foreign key constraint fails
            REFERENCED_RECORD_DOES_NOT_EXIST => (400, "Referenced record does not exist"),
            // Cannot delete or update (referenced by foreign key)
            CANNOT_DELETE_OR_UPDATE => (409, "Cannot delete this record as it is referenced by other records"),
            // Data too long for column
            DATA_TOO_LONG_FOR_COLUMN => (400, "Data too long for the specified field"),
            _ => (DATABASE_ERROR, "Database error occurred")
        };
    }

    private static string ExtractDuplicateEntryMessage(string errorMessage)
    {
        // MySQL error format: "Duplicate entry 'value' for key 'constraint_name'"
        var match = Regex.Match(
            errorMessage,
            @"Duplicate entry ['`]?([^'`]+)['`]? for key ['`]?(\w+)['`]?");

        if (match.Success)
        {
            var value = match.Groups[1].Value;
            var constraint = match.Groups[2].Value;
            return $"A record with value '{value}' already exists (constraint: {constraint})";
        }

        return "A record with this value already exists";
    }
}