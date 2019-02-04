using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Bo;
using WebShop.Web.Interfaces;
using WebShop.Web.Models;

namespace WebShop.Web.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public Order GetInformation(string ssn)              //Metod som ska ladda uppgifter på ssn
        {
            var information = new Order();
            information.FirstName = "Daniel";
            information.LastName = "Jareteg";
            information.Adress = "123";
            information.ZipCode = "543 33";
            information.City = "Borås";
            information.Country = "Sweden";

            return information;
        }

        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;

        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some products first");
            }

            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }

            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order!";
            return View();
        }
    }



    
}