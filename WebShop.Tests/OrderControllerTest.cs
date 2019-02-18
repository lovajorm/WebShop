using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using WebShop.Dal.Interfaces;
using WebShop.Dal.UoW;
using WebShop.Web.Controllers;

namespace WebShop.Tests
{
    public class OrderControllerTest
    {
        protected Mock<UnitOfWork> UnitOfWorkMock { get; }
        protected OrderController ControllerToTest { get; }
        public OrderControllerTest()
        {
            UnitOfWorkMock = new Mock<UnitOfWork>();
            //ControllerToTest = new OrderController(UnitOfWorkMock,null,null);
        }

        
    }
}
