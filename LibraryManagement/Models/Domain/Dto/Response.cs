namespace LibraryManagement.Models.Domain.Dto
{
    public class Response<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
