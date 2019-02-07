using System.IO;
using System.Net.Http;
using System.Reflection;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Moq;
using WebShop.Dal;
using WebShop.Log;
using WebShop.Web.Controllers;
using WebShop.Web.Interfaces;
using Xunit;

namespace WebShop.Tests
{
    public class LoggerTests
    {
        private readonly WebShopDbContext _context;
        private readonly Log.IMessageLogger _logger;
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly Mock<IProductRepository> _productRepository;
        public LoggerTests()
        {
            _categoryRepository = new Mock<ICategoryRepository>();
            _productRepository = new Mock<IProductRepository>();
        }

        [Fact]
        public void TestLog()
        {
            // Arrange
            var messageLogger = new MessageLogger();

            messageLogger.LogInfo("there", "here and not there");
            
        }

        [Fact]
        public void TestLo2g()
        {
            // Arrange
            var messageLogger = new MessageLogger();

            messageLogger.LogInfo("there", "here and not there");

        }
    }
}
