namespace Sellius.API.DTOs
{
    public class Response<T>
    {
        public bool success { get; set; }
        public string? errorMessage { get; set; }
        public string? message { get; set; } = "Sucesso na requisição";
        public int? statusCode { get; set; }
        public T Data { get; set; }

        public static Response<T> Ok(T data,string message)
        {
            return new Response<T> { success = true, Data = data , message = message};
        }
       public static Response<T> Ok(T data)
        {
            return new Response<T> { success = true, Data = data};
        }
        public static Response<T> Ok()
        {
            return new Response<T> { success = true};
        }
        public static Response<T> Failed(string errorMesssge)
        {
            return new Response<T> { success = false, errorMessage = errorMesssge};
        }

        public static Response<T> Failed(Exception ex)
        {
            return new Response<T> { success = false, errorMessage = ex.Message};
        }
    }
}
