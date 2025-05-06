namespace BackEndChellengeAPI.Responses
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
