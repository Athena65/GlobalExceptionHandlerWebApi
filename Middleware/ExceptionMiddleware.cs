using GlobalExceptionHandlerWebApi.Exceptions;
using GlobalExceptionHandlerWebApi.Models;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
using System.Net;

namespace GlobalExceptionHandlerWebApi.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				string errorId=Guid.NewGuid().ToString();
				LogContext.PushProperty("ErrorId", errorId);
				LogContext.PushProperty("StackTrace", ex.StackTrace);
				var errorResult = new ErrorResult
				{
					Source = ex.TargetSite?.DeclaringType?.FullName,
					Exception = ex.Message.Trim(),
					ErrorId = errorId,
					SupportMessage = $"Provide the Error Id:{errorId} to the support team for further analysis."
				};
				errorResult.Messages.Add(ex.Message);
				if(ex is not CustomException && ex.InnerException != null) 
				{ 
					while(ex.InnerException != null)
					{
						ex=ex.InnerException;
					}

				}
				switch(ex)
				{
					case CustomException e:
						errorResult.StatusCode = (int)e.StatusCode;
						if(e.ErrorMessages is not null)
						{
							errorResult.Messages = e.ErrorMessages;
						}
						break;
					case KeyNotFoundException:
						errorResult.StatusCode=(int)HttpStatusCode.NotFound;
						break;
					default:
						errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
						break;	
				}
				Log.Error($"{errorResult.Exception} Request failed with Status Code {context.Response.StatusCode} and Error Id is {errorId}.");
				var response= context.Response;
				if (!response.HasStarted)
				{
					response.ContentType = "application/json";	
					response.StatusCode=errorResult.StatusCode;
					await response.WriteAsync(JsonConvert.SerializeObject(errorResult));
				}
				else
				{
					Log.Warning("Can't write error response.Response has already started");
				}
			}
        }
    }
}
