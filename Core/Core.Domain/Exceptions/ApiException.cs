namespace Core.Domain.Exceptions
{
    public class ApiException : Exception
    {
        public int ErrorCode { get; set; }

        public ApiException(string message) : base(message) { }

        public ApiException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        public ApiException(string message, int errorCode, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
