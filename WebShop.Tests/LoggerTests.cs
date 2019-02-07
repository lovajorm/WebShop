using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using Moq;
using WebShop.Log;
using WebShop.Web.Middleware;
using Xunit;

namespace WebShop.Tests
{
    public class LoggerTests
    {
        private readonly ILoggerRepository _logRepository;
        public LoggerTests()
        {
            _logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(_logRepository, new FileInfo("..\\WebShop.Log\\log4net.config"));
        }

        [Fact]
        public async void It_should_log_request()
        {
            var logger = new MessageLogger();
            logger.LogInfo(this.GetType().Name, "testmessage");
            

        }
    }
}
