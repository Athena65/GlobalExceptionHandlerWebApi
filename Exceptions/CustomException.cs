using System.Net;

namespace GlobalExceptionHandlerWebApi.Exceptions
{
    public class CustomException:Exception
    {
        public List<string>? ErrorMessages { get; set; }

        public HttpStatusCode StatusCode { get; set; }
        public CustomException(string message,List<string>? errors=default, HttpStatusCode statusCode=HttpStatusCode.InternalServerError):base(message)
        {
            ErrorMessages = errors;
            StatusCode = statusCode;
        }
    }
}
