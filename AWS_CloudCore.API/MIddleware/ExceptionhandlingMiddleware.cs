using AWS_CloudCore.API.Models;
using AWS_CloudCore.Core.Common.Enums;
using AWS_CloudCore.Core.Common.Exceptions;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AWS_CloudCore.API.MIddleware
{
    public class ExceptionHandlingMiddleware
    {
        public ExceptionHandlingMiddleware(ILoggerFactory loggerFactory, RequestDelegate next)
        {
            Logger = loggerFactory.CreateLogger(nameof(ExceptionHandlingMiddleware));
            Next = next;
        }

        public ILogger Logger { get; }
        public RequestDelegate Next { get; }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            Guid LogId = Guid.NewGuid();
            try
            {
                await Next(httpContext);
            }
            catch (ReturnExceptionMessageToCallerException ex)
            {
                await HandleExceptionAsync(httpContext, new ExceptionResponseModel
                {
                    LogMessage = $"{ex.Message}, Query: {ex.Details}",
                    StatusCode = HttpStatusCode.InternalServerError,
                    ReponseMessage = ex.Message
                });
            }
           
            catch (DatabaseException ex) when (ex.Status == ExceptionStatus.Database)
            {
                await HandleExceptionAsync(httpContext, new ExceptionResponseModel
                {
                    LogMessage = $"{ex.Message}, Query: {ex.Query}, StackTrace: {ex.StackTrace}",
                    StatusCode = HttpStatusCode.InternalServerError,
                    ReponseMessage = $"Error while executing a database query"
                });
            }
            catch (DatabaseException ex) when (ex.Status == ExceptionStatus.Validation)
            {
                await HandleExceptionAsync(httpContext, new ExceptionResponseModel
                {
                    LogMessage = $"{ex.Message}, details: {ex.Details}, StackTrace: {ex.StackTrace}",
                    StatusCode = HttpStatusCode.BadRequest,
                    ReponseMessage = ex.Message
                });
            }
            catch (InputValidationException ex)
            {
                await HandleExceptionAsync(httpContext, new ExceptionResponseModel
                {
                    LogMessage = $"{ex.Message}, details: {ex.Details}, StackTrace: {ex.StackTrace}",
                    StatusCode = HttpStatusCode.BadRequest,
                    ReponseMessage = ex.Message
                });
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, new ExceptionResponseModel
                {
                    LogMessage = $"{ex.Message}, Stack Trace:{ex.StackTrace}, Inner: {ex.InnerException}",
                    ReponseMessage = "Something Went Wrong"
                });
            }
            Task HandleExceptionAsync(HttpContext context, ExceptionResponseModel exceptionResponse)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                Logger.LogError($"LogId:{LogId}: Message: {exceptionResponse.LogMessage}");
                return context.Response.WriteAsync(new ErrorDetails()
                {
                    Message = $"{exceptionResponse.ReponseMessage} ,LogId: {LogId}"
                }.Serialize());
            }
        }
    }
}
