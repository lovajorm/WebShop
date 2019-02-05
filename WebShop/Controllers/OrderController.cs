using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using WebShop.Bo;
using WebShop.Models;
using WebShop.Web.Interfaces;
using WebShop.Web.Models;


namespace WebShop.Web.Controllers
{
    public class OrderController : Controller
    {

        public IActionResult GetInformation(Order ssn)              //Method which gets customer information by using Ssn, see checkout.cshtml.
        {
            
            Customer response = new Customer();                         //Initializes customer

            using (var handler = new WebRequestHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    var bytearray = Encoding.ASCII.GetBytes("Testpartner Sweden:123456");

                    //Sends authentication to client.
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytearray));

                    //Sends a json-object and gets "content" in response.
                    try
                    {
                        var result = client.GetStringAsync(new Uri("https://stage.avarda.org/WebShopApi/webshop/ssn/swe/196504192383")).Result;

                        response = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(result);                  //Converts from json to c# text.
                    }


                    catch (WebException e)
                    {

                    }
                }

            }
            return View(ssn);

        }

        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;

        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }


        public IActionResult Checkout()                         //"Check out" from shopping cart to information form.
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;


            if (_shoppingCart.ShoppingCartItems.Count == 0)                     //Check to see if the shoppingc art contains any items.
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Your cart is empty, add some products first" });                  //Error message shown if you try to check out order without any items in the cart.
            }

            return View();


        }

        [HttpPost]
        public IActionResult Checkout(Order order)                                //Happens when user presses "Complete Order".
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (ModelState.IsValid)                                             //If shopping cart is okay, ....
            {
                _orderRepository.CreateOrder(order);                            //Calls the method CreateOrder in OrderRepository.
                _shoppingCart.ClearCart();                                      //Clears cart after "Complete order"
                return RedirectToAction("CheckoutComplete");
            }

            return View(order);
        }

        public IActionResult CheckoutComplete()                 //Text shown after you click "Complete Order".
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order!";
            return View();
        }
    }




}