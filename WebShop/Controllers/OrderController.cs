using System;
using Microsoft.AspNetCore.Mvc;
using WebShop.Avarda.Api;
using WebShop.Avarda.Api.Avarda;
using WebShop.Bo;
using WebShop.Models;
using WebShop.Web.Interfaces;
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
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly IEmailHandler _emailHandler;
        private ConnectionHandler _connectionHandler;
        private readonly IProductRepository _productRepository;

        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart, IEmailHandler emailHandler, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _emailHandler = emailHandler;
            _connectionHandler = new ConnectionHandler();
            _productRepository = productRepository;
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
               _orderRepository.CreateOrder(order, response);
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