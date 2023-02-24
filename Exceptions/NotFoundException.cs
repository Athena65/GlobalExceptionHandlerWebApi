using System.Net;

namespace GlobalExceptionHandlerWebApi.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) 
            : base(message,null, HttpStatusCode.NotFound) { }

    }
}
