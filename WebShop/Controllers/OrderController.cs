using System;
using Microsoft.AspNetCore.Mvc;
using WebShop.Avarda.Api;
using WebShop.Avarda.Api.Avarda;
using WebShop.Bo;
using WebShop.Dal.UoW;
using WebShop.Models;
using WebShop.Web.Models;
using WebShop.Common;
using System.Collections.Generic;

namespace WebShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private IUnitOfWork _unitOfWork;
        private ConnectionHandler _getCustomer;
        private readonly IEmailHandler _emailHandler;
        private ConnectionHandler _connectionHandler;

        public OrderController(IUnitOfWork unitOfWork, ShoppingCart shoppingCart, IEmailHandler emailHandler)
        {
            _shoppingCart = shoppingCart;
            _emailHandler = emailHandler;

            _connectionHandler = new ConnectionHandler();
            _unitOfWork = unitOfWork;
            
        }

        [HttpGet]
        public IActionResult InitializePayment(PaymentRequest request)
        {
            request.Price = _shoppingCart.GetShoppingCartTotal();
            request.Items = ConvertShoppingCartItemToItem();

            try
            {
                var response = _connectionHandler.InitializePayment(request);

                return View("Avarda", response);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = $"Something went wrong: {ex.Message}" });
            }
        }

        private List<Item> ConvertShoppingCartItemToItem()
        {
            var itemList = new List<Item>();

            foreach (var item in _shoppingCart.GetShoppingCartItems())
            {
                itemList.Add(new Item
                {
                    Amount = (int)item.Product.Price,
                    Description = item.Product.Title
                });
            }
            return itemList;
        }

        public IActionResult Done(string purchaseId, Order order)
        {
            var response = _connectionHandler.GetPaymentStatus(purchaseId);

            if (response.State == 2)
            {
                var shoppingCartItems = _shoppingCart.GetShoppingCartItems();
               _unitOfWork.Order.CreateOrder(order, response, shoppingCartItems);
                switch (response.PaymentMethod)
                {
                    case PaymentMethodEnum.Invocie:
                    case PaymentMethodEnum.Loan:
                        ViewData["description"] = purchaseId;
                        return View();
                    default:
                        return View("CheckoutComplete");
                }
            }
            return View("Error", new ErrorViewModel { ErrorMessage = $"Payment failed." });
        }
    }
}