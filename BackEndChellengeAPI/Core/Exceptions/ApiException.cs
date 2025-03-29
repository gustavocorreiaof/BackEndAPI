namespace Core.Exceptions // Mudança de Core.Exception para Core.Exceptions
{
    public class ApiException : Exception
    {
        // Propriedade para armazenar um código de erro (caso necessário)
        public int ErrorCode { get; set; }

        // Construtor que aceita apenas uma mensagem de erro
        public ApiException(string message) : base(message) { }

        // Construtor que aceita uma mensagem de erro e um código de erro
        public ApiException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        // Construtor que aceita uma mensagem de erro, um código de erro e uma exceção interna (encadeamento)
        public ApiException(string message, int errorCode, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
