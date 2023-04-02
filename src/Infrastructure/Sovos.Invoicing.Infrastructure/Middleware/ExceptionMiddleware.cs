using System.Net;
using System.Text.Json;

using Microsoft.AspNetCore.Http;

using Serilog;
using Serilog.Context;

using Sovos.Invoicing.Domain.Core.Exceptions;

namespace Sovos.Invoicing.Infrastructure.Middleware;

internal class ExceptionMiddleware : IMiddleware
{

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            string errorId = Guid.NewGuid().ToString();
            LogContext.PushProperty("ErrorId", errorId);
            LogContext.PushProperty("StackTrace", exception.StackTrace);
            var errorResult = new ErrorResult
            {
                Source = exception.TargetSite?.DeclaringType?.FullName,
                Exception = exception.Message.Trim(),
                ErrorId = errorId,
                SupportMessage = $"Provide the ErrorId {errorId} to the support team for further analysis."
            };
            errorResult.Messages.Add(exception.Message);
            if (exception is not SovosException && exception.InnerException != null)
            {
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                }
            }

            errorResult.StatusCode = exception switch
            {
                BadRequestException => (int)HttpStatusCode.BadRequest,
                ConflictException => (int)HttpStatusCode.Conflict,
                ForbiddenException => (int)HttpStatusCode.Forbidden,
                UnAuthorizedException => (int)HttpStatusCode.Unauthorized,
                NotFoundException or KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError,
            };

            Log.Error($"{errorResult.Exception} Request failed with Status Code {context.Response.StatusCode} and Error Id {errorId}.");
            var response = context.Response;
            if (!response.HasStarted)
            {
                response.ContentType = "application/json";
                response.StatusCode = errorResult.StatusCode;
                await response.WriteAsync(JsonSerializer.Serialize(errorResult));
            }
            else
            {
                Log.Warning("Can't write error response. Response has already started.");
            }
        }
    }
}