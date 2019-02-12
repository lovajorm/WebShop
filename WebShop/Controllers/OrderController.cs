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

namespace WebShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly IEmailHandler _emailHandler;
        private ConnectionHandler _connectionHandler;


        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart, IEmailHandler emailHandler)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _emailHandler = emailHandler;

            _connectionHandler = new ConnectionHandler();
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

        [HttpGet]
        public JsonResult GetInformation(string ssn)                             //Method which gets customer information by using Ssn, see checkout.cshtml.
        {
            try
            {
                var info = _connectionHandler.GetCustomerInfo(ssn);
                return Json(info);
            }
            catch (Exception ex)
            
            {
                return Json(new ErrorViewModel { ErrorMessage = $"Failed to get customer. Error: {ex.Message}" });      //If exception is caught, will show error message
            }
        }

        [HttpPost]                                                                          //send authorization to web api
        public IActionResult AuthorizeInvoice(InvoiceRequest request, Order order)
        {
            var total = _shoppingCart.GetShoppingCartTotal();
            order.OrderTotal = total;

            try
            {
                var response = _connectionHandler.AuthorizeInvoice(request, order);

                if (total < response.Result.CreditLimit)                                                //In tests the creditLimit is not fixed, which means that the if-statement is unnecessary but we will keep it for fun.
                {
                    order.OrderDetails = _orderRepository.CreateOrder(order);                           //Save order and take customer to final page
                    _shoppingCart.ClearCart();                                                          //Clears cart after "Complete order"
                    _emailHandler.SendEmail();                                                          //Sends email to customer
                    return View("CheckoutComplete", order);
                }
                else
                {
                    return View("Error", new ErrorViewModel { ErrorMessage = $"Your credit score is too low" });
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = $"Couldn't get credit score. Error Message: {ex.Message}" });
            }
        }

        public IActionResult Checkout()                                                                                 //"Check out" from shopping cart to information form.
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0)                                                                 //Check to see if the shopping cart contains any items.
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Your cart is empty, add some products first" }); //Error message shown if you try to check out order without any items in the cart.
            }
            return View();
        }
    }
}