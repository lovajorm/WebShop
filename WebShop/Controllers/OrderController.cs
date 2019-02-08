using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebShop.Avarda.Api;
using WebShop.Avarda.Api.Avarda;
using WebShop.Bo;
using WebShop.Models;
using WebShop.Web.Interfaces;
using WebShop.Web.Models;

namespace WebShop.Web.Controllers
{
    public class OrderController : Controller

    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;
        private GetCustomer _getCustomer;

        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _getCustomer = new GetCustomer();
        }

        [HttpGet]
        public JsonResult GetInformation(string ssn)              //Method which gets customer information by using Ssn, see checkout.cshtml.
        {
            try
            {
                var info = _getCustomer.GetCustomerInfo(ssn);
                return Json(info);
            }
            catch (Exception ex)
            //If exception is caught, will show error message
            {
                return Json(new ErrorViewModel { ErrorMessage = $"Failed to get customer. Error: {ex.Message}" });
            }

        }

        [HttpPost] //send authorization to web api
        public async Task<IActionResult> AuthorizeInvoice(InvoiceRequest request, Order order)
        {
            try
            {
                var invoice = _getCustomer.Au
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = $"Couldn't get credit score. Error Message: {ex.Message}" });
            }
        }

        public IActionResult Checkout()                         //"Check out" from shopping cart to information form.
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0)                     //Check to see if the shopping cart contains any items.
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Your cart is empty, add some products first" });                  //Error message shown if you try to check out order without any items in the cart.
            }

            return View();
        }
    }
}