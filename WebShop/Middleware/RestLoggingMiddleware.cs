using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using WebShop.Log;

namespace WebShop.Web.Middleware
{
    public class RestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMessageLogger _logger;

        public RestLoggingMiddleware(RequestDelegate next, IMessageLogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInfo(this.GetType().FullName,await FormatRequest(context.Request));

            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                try
                {
                    await _next(context);
                }
                catch (Exception e)
                {
                    _logger.LogError(this.GetType().FullName, e);
                }

                var response = await FormatResponse(context.Response);
                if (context.Response.ContentType != null)
                {
                    if (context.Response.ContentType.Contains("application/json"))
                    {
                        _logger.LogInfo(this.GetType().FullName, response);
                    }
                    else
                    {
                        _logger.LogInfo(this.GetType().FullName, $"{context.Response.StatusCode}");
                    }
                }

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatResponse(HttpResponse contextResponse)
        {
            contextResponse.Body.Seek(0, SeekOrigin.Begin);
            string text = await new StreamReader(contextResponse.Body).ReadToEndAsync();
            contextResponse.Body.Seek(0, SeekOrigin.Begin);

            return $"{contextResponse.StatusCode}: {text}";
        }

        private async Task<string> FormatRequest(HttpRequest contextRequest)
        {
            var body = contextRequest.Body;
            contextRequest.EnableRewind();

            var buffer = new byte[Convert.ToInt32(contextRequest.ContentLength)];
            await contextRequest.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            contextRequest.Body = body;

            return
                $"{contextRequest.Scheme} {contextRequest.Host}{contextRequest.Path}{contextRequest.QueryString}{bodyAsText}";
        }
    }
}
