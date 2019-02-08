using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using WebShop.Log;

namespace WebShop.Web.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMessageLogger _logger;

        public LoggingMiddleware(RequestDelegate next, IMessageLogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var guid = Guid.NewGuid();
            var stopwatch = Stopwatch.StartNew();
            var request = await FormatRequest(context.Request);

            LogInformation($"{guid} Request: {request}");

            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                try
                {
                    await _next(context);
                    stopwatch.Stop();
                }
                catch (Exception e)
                {
                    _logger.LogError(this.GetType().Name, e);
                }

                var response = await FormatResponse(context.Response);

                if (context.Response.ContentType != null)
                {

                    if (context.Response.ContentType.Contains("application/json"))
                    {
                        LogInformation($"{guid} ResponseTime: {stopwatch.ElapsedMilliseconds} Response: {response}");
                    }
                    else
                    {
                        LogInformation($"{guid} ResponseTime: {stopwatch.ElapsedMilliseconds} Response: {context.Response.StatusCode}");
                    }
                }

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;

            request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            //request.Body = body;
            request.Body.Position = 0;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            string text = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            return $"{response.StatusCode}: {text}";
        }

        private void LogInformation(string message)
        {
            _logger.LogInfo(this.GetType().FullName, message);
        }

    }
}
