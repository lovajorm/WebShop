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
using System.Linq;
using WebShop.Dal.Migrations;
using WebShop.Web.Repositories;
using WebShop.Web.ViewModels;

namespace WebShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private IUnitOfWork _unitOfWork;
        private ConnectionHandler _getCustomer;
        private readonly IEmailHandler _emailHandler;
        private ConnectionHandler _connectionHandler;
        private readonly IProductRepository _productRepository;

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

 
        public IActionResult Done(string purchaseId, Order order, Product product)
        {
            var response = _connectionHandler.GetPaymentStatus(purchaseId);

            product = _productRepository.Products.FirstOrDefault(p => p.ProductID.Equals(5));

            var purchaseViewModel = new ExtraPurchaseViewModel
            {
                Product = product,
                PurchaseId = purchaseId,
                ProductId = product.ProductID
            };

            if (response.State == 2)
            {
                var shoppingCartItems = _shoppingCart.GetShoppingCartItems();
               _unitOfWork.Order.CreateOrder(order, response, shoppingCartItems);
                switch (response.PaymentMethod)
                {
                    case PaymentMethodEnum.Invocie:
                    case PaymentMethodEnum.Swish:
                        ViewData["description"] = purchaseId;
                        return View(purchaseViewModel);
                    default:
                        return View("CheckoutComplete", order);
                }
            }
            _shoppingCart.ClearCart();
            return View("Error", new ErrorViewModel { ErrorMessage = $"Payment failed." });
        }

        public IActionResult PurchaseOrder(ExtraPurchaseViewModel purchaseViewModel)
        {
            var request = new PurchaseOrderRequest();

            request.ExternalId = purchaseViewModel.PurchaseId;
            request.Items = ConvertShoppingCartItemToItem();

            _connectionHandler.PurchaseOrder(request);

            return View("CheckoutComplete" /*Add order*/);
        }
    }
}