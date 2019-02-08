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
        private readonly GetCustomer _getCustomer;

        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart, GetCustomer getCustomer)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _getCustomer = getCustomer;
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
            using (var handler = new WebRequestHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    var bytearray = Encoding.ASCII.GetBytes("Testpartner Sweden:123456");

                    //sets authentication header.
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytearray));

                    try
                    {
                        client.BaseAddress =
                            new Uri($"https://stage.avarda.org/");

                        var total = _shoppingCart.GetShopppingCartTotal();
                        request.Amount = total;
                        request.Country = "Swe";

                        var jsonRequest = JsonConvert.SerializeObject(request);

                        //Serializes the class to json
                        var body = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                        var result = await client.PostAsync("WebShopApi/webshop/authorization/invoice", body);                                          //Calling Avarda API and sending the json

                        if (!result.IsSuccessStatusCode)
                        {
                            throw new Exception(result.Content.ReadAsStringAsync().Result);
                        }

                        var response = JsonConvert.DeserializeObject<InvoiceResponse>(result.Content.ReadAsStringAsync().Result);


                        var items = _shoppingCart.GetShoppingCartItems();
                        _shoppingCart.ShoppingCartItems = items;

                        if (total < response.CreditLimit)                                   //In tests the creditLimit is not fixed, which means that the if-statement is unnecessary but we will keep it for fun.
                        {
                            //Save order and take customer to final page
                            order.OrderDetails = _orderRepository.CreateOrder(order);
                            _shoppingCart.ClearCart();
                            //Clears cart after "Complete order"
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
            }
            //return View("Checkout");
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