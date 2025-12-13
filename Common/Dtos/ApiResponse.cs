namespace Saturday_Back.Common.Dtos
{
    /// <summary>
    /// Generic API response wrapper that matches the error response format
    /// Can be used for both success and error responses
    /// </summary>
    public class ApiResponse
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public object? Data { get; set; }
    }
}