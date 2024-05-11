using AutoPiter.Domain.Exceptions;
using AutoPiter.Infrastructure.Responses;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net;
using System.Text.Json;

namespace AutoPiter.Infrastructure.Middleware
{
    public class ErrorHandlerMiddleware(RequestDelegate next,
                                        ILogger<ErrorHandlerMiddleware> logger,
                                        IHostEnvironment env)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger = logger;
        private readonly IHostEnvironment _env = env;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiException ex)
            {
                await HandleApiExceptionAsync(context, ex);
            }
            catch(DbUpdateException ex)
            {
                await HandleDbExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleDbExceptionAsync(HttpContext context, DbUpdateException ex)
        {
            if (!string.IsNullOrWhiteSpace(ex.Message))
                _logger.LogError(ex, ex.Message);

            return WriteResponseAsync(context,
                                      HttpStatusCode.InternalServerError,
                                      ex.Message,
                                      "Произошла ошибка на стороне базы данных",
                                      ex.StackTrace ?? ""
                                      );
        }

        private Task HandleApiExceptionAsync(HttpContext context, ApiException ex)
        {
            if (string.IsNullOrEmpty(ex.ErrorMessage))
                _logger.LogError(ex.ErrorMessage);
            if (string.IsNullOrEmpty(ex.UserFriendlyMessage))
                _logger.LogError(ex.UserFriendlyMessage);


            return WriteResponseAsync(context,
                                      HttpStatusCode.InternalServerError,
                                      ex.ErrorMessage,
                                      ex.UserFriendlyMessage,
                                      ex.StackTrace ?? ""
                                      );
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            if (!string.IsNullOrWhiteSpace(ex.Message))
                _logger.LogError(ex, ex.Message);

            var userFriendlyMessage = "Internal Server Error";

            return WriteResponseAsync(context,
                                      HttpStatusCode.InternalServerError,
                                      ex.Message,
                                      userFriendlyMessage,
                                      ex.StackTrace ?? ""
                                      );
        }

        private Task WriteResponseAsync(HttpContext context,
                                        HttpStatusCode code,
                                        string errorMessage,
                                        string userFriendlyMessage,
                                        string details)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.StatusCode = (int)code;

            var errorResponse = new ErrorResponse(errorMessage, userFriendlyMessage, details);

            var serializerSettings = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            string json = JsonSerializer.Serialize(errorResponse, serializerSettings);

            return context.Response.WriteAsJsonAsync(json);
        }
    }
}
