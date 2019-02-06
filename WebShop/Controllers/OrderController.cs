using System;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebShop.Bo;
using WebShop.Models;
using WebShop.Web.Interfaces;
using WebShop.Web.Models;
using WebShop.Web.Models.Avarda;


namespace WebShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart; 

        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }

        [HttpGet]
        public JsonResult GetInformation(string ssn)              //Method which gets customer information by using Ssn, see checkout.cshtml.
        {
            
            Customer response;                         //Initializes customer

            using (var handler = new WebRequestHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    var bytearray = Encoding.ASCII.GetBytes("Testpartner Sweden:123456");

                    //sets authentication header.
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytearray));

                    //Sends a json-object and gets back result, converts result from json to c# which is "response"
                    try
                    {
                        var result = client.GetStringAsync(new Uri($"https://stage.avarda.org/WebShopApi/webshop/ssn/swe/{ssn}")).Result;
                        response = JsonConvert.DeserializeObject<Customer>(result);                  //Converts from json to c# class.
                    }
                    catch (Exception ex)                    //If exception is caught, will show error message
                    {
                        return Json(new ErrorViewModel { ErrorMessage = $"Failed to get customer. Error: {ex.Message}" });
                    }
                }
            }
            return Json(response);
        }

        [HttpPost]//send authorization to web api
        public IActionResult AuthorizeInvoice(InvoiceRequest request, Order order)
        {;

            using (var handler = new WebRequestHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    var bytearray = Encoding.ASCII.GetBytes("Testpartner Sweden:123456");

                    //sets authentication header.
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytearray));

                    try
                    {
                        var total = _shoppingCart.GetShopppingCartTotal();
                        request.Amount = total;

                        var jsonRequest = JsonConvert.SerializeObject(request);             //Serializes the class to json
                        var result = client.PostAsJsonAsync(new Uri($"https://stage.avarda.org/WebShopApi/webshop/authorization/invoice"), request).Result;             //Calling Avarda API

                        var response = JsonConvert.DeserializeObject<InvoiceResponse>(result.Content.ReadAsStringAsync().Result);

                        
                        if (total < response.CreditLimit)
                        {
                            //Save order and take customer to final page
                            _orderRepository.CreateOrder(order);
                            _shoppingCart.ClearCart();                                      //Clears cart after "Complete order"
                            return RedirectToAction("CheckoutComplete");
                        }
                        else 
                        {
                            //Show error message displaying credit score is too low could not place order
                        }

                        
                    }
                    catch(Exception ex)
                    {
                        return View("Error", new ErrorViewModel { ErrorMessage = $"Couldn't get credit score. Error Message: {ex.Message}" });
                    }
                }
            }
            return View("Checkout");
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

        //[HttpPost]
        //public IActionResult Checkout(Order order)                                //Happens when user presses "Complete Order".
        //{
        //    var items = _shoppingCart.GetShoppingCartItems();
        //    _shoppingCart.ShoppingCartItems = items;

        //    if (ModelState.IsValid)                                             //If shopping cart is okay, ....
        //    {
        //        _orderRepository.CreateOrder(order);                            //Calls the method CreateOrder in OrderRepository.
        //        _shoppingCart.ClearCart();                                      //Clears cart after "Complete order"
        //        return RedirectToAction("CheckoutComplete");
        //    }

        //    return View(order);
        //}

        public IActionResult CheckoutComplete()                 //Text shown after you click "Complete Order".
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order!";
            return View();
        }
    }
}