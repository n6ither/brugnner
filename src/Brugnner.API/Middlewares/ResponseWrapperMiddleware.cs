using Brugnner.API.Core.Exceptions;
using Brugnner.API.Core.Extensions;
using Brugnner.API.ResponseWrapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Brugnner.API.Middlewares
{
    /// <summary>
    /// Middleware that wraps the API responses with an <see cref="APIResponse"/> object.
    /// </summary>
    public class ResponseWrapperMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseWrapperMiddleware> _logger;
        private readonly IHostingEnvironment _environment;

        /// <summary>
        /// Creates a new instance of <see cref="ResponseWrapperMiddleware"/>.
        /// </summary>
        /// <param name="next">Next Http request.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="environment">Hosting environment.</param>
        public ResponseWrapperMiddleware(RequestDelegate next, ILogger<ResponseWrapperMiddleware> logger, IHostingEnvironment environment)
        {
            _next = next;
            _logger = logger.ThrowIfNull(nameof(logger));
            _environment = environment.ThrowIfNull(nameof(environment));
        }

        /// <summary>
        /// Invokes the middleware and wrap the API response.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var originalResponseBody = context.Response.Body;

            using (var newResponseBody = new MemoryStream())
            {
                try
                {
                    context.Response.Body = newResponseBody;
                    context.Response.ContentType = "application/json";

                    await _next.Invoke(context);
                    await HandleResponseAsync(context);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Something went wrong");
                    await HandleExceptionAsync(context, ex);
                }
                finally
                {
                    await WriteNewResponseAsync(originalResponseBody, newResponseBody);
                }
            }
        }

        private async Task HandleResponseAsync(HttpContext context)
        {
            var responseBodyString = await GetResponseAsStringAsync(context.Response);
            var result = JsonConvert.DeserializeObject<object>(responseBodyString);
            var apiResponse = new APIResponse((HttpStatusCode)context.Response.StatusCode, result: result);
            var jsonResult = JsonConvert.SerializeObject(apiResponse);

            await context.Response.WriteAsync(jsonResult);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            APIError error = null;
            HttpStatusCode code = 0;

            if (exception is BusinessException)
            {
                var ex = exception as BusinessException;
                error = new APIError(ex.Message);
                code = HttpStatusCode.BadRequest;
                context.Response.StatusCode = (int)code;
            }
            else if (exception is ResourceNotFoundException)
            {
                var ex = exception as ResourceNotFoundException;
                error = new APIError(ex.Message);
                code = HttpStatusCode.NotFound;
                context.Response.StatusCode = (int)code;
            }
            else if (exception is APIException)
            {
                var ex = exception as APIException;
                error = new APIError(ex.Message, ex.Errors);
                code = (HttpStatusCode)ex.StatusCode;
                context.Response.StatusCode = (int)code;
            }
            else
            {
                string message = _environment.IsDevelopment() ? exception.GetBaseException().Message : "An unhandled error occurred.";
                string stackTrace = _environment.IsDevelopment() ? exception.StackTrace : null;

                error = new APIError(message, stackTrace);
                code = HttpStatusCode.InternalServerError;
                context.Response.StatusCode = (int)code;
            }

            var apiResponse = new APIResponse(code, error: error);
            var json = JsonConvert.SerializeObject(apiResponse);

            return context.Response.WriteAsync(json);
        }

        private async Task WriteNewResponseAsync(Stream originalResponseBody, MemoryStream newResponseBody)
        {
            newResponseBody.Seek(0, SeekOrigin.Begin);
            await newResponseBody.CopyToAsync(originalResponseBody);
        }

        private async Task<string> GetResponseAsStringAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string responseBodyString = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return responseBodyString;
        }

        private string GetDescription(int statusCode)
        {
            var description = ((HttpStatusCode)statusCode).ToString();
            string[] split = Regex.Split(description, @"(?<!^)(?=[A-Z])");
            string result = split[0];

            foreach (var item in split.Skip(1))
                result += " " + item.ToLower();

            return result;
        }
    }
}
