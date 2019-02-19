using Microsoft.AspNetCore.Mvc;
using Moq;
using WebShop.Bo;
using WebShop.Dal.Interfaces;
using WebShop.Dal.Repositories;
using WebShop.Dal.UoW;
using WebShop.Web.Controllers;
using Xunit;

namespace WebShop.Tests
{
    public class ProductControllerTest
    {
        //[Fact]
        //public void TestDetailsView()
        //{
        //    var product = new Product();
        //    product.ProductID = 2;
        //    var unitOfWorkMock = new Mock<IUnitOfWork>();
        //    var IProductRepositoryMock = new ProductRepository();
        //    unitOfWorkMock.Object.Product.Add(product);
        //    var controller = new ProductController(null, null, unitOfWorkMock.Object);
        //    var result = controller.Details(2) as ViewResult;
        //    Assert.Equal("Details", result.ViewName);
        //}

        //[Fact]
        //public void TestDetailsViewData()
        //{
        //    var controller = new ProductController(null, null, null);
        //    var result = controller.Details(1) as ViewResult;
        //    var product = (Product) result.ViewData.Model;
        //    Assert.Equal("Sweater", product.Title);
        //}
        [Fact]
        public void TestDetailsView()
        {
            var controller = new ProductController(null, null, null);
            var result = controller.Details(1) as ViewResult;
            Assert.Equal("Details", result.ViewName);
        }
    }
}
