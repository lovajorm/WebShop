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
            using (var responseBody = new MemoryStream())
            {
                _logger.LogInfo(this.GetType().FullName, context.Request.Body.ToString());

                try
                {
                    await _next(context);
                }
                catch (Exception e)
                {
                    _logger.LogError(this.GetType().FullName, e);
                }
                if (context.Response.ContentType != null)
                {
                    if (context.Response.ContentType.Contains("application/json"))
                    {
                        _logger.LogInfo(this.GetType().FullName, context.Response.Body.ToString());
                    }
                    else
                    {
                        _logger.LogInfo(this.GetType().FullName, $"{context.Response.StatusCode}");
                    }
                }                
            }
        }
    }
}
