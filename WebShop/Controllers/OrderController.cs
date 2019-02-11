using System;
using Microsoft.AspNetCore.Mvc;
using WebShop.Avarda.Api;
using WebShop.Avarda.Api.Avarda;
using WebShop.Bo;
using WebShop.Models;
using WebShop.Web.Interfaces;
using WebShop.Web.Models;
using WebShop.Common;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly IEmailHandler _emailHandler;
        private ConnectionHandler _getCustomer;


        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart, IEmailHandler emailHandler)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _emailHandler = emailHandler;            
       
            _getCustomer = new ConnectionHandler();
        }

        [HttpPost]
        public async Task<IActionResult> InitializePayment()
        {
            var request = new PaymentRequest();
            using (var handler = new WebRequestHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    var bytearray = Encoding.ASCII.GetBytes("TestSweden1:test1");               //Authentication

                    //sets authentication header.
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytearray));

                    client.BaseAddress = new Uri($"https://stage.avarda.org/");

                    request.Price = _shoppingCart.GetShoppingCartTotal();
                    request.Items = ConvertShoppingCartItemToItem();

                    //var jsonRequest = JsonConvert.SerializeObject(request);

                    //Serializes the class to json
                    var body = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync("CheckOut2/CheckOut2Api/InitializePayment", body);                                          //Calling Avarda API and sending the json

                    if (!result.IsSuccessStatusCode)
                    {
                        throw new Exception(result.Content.ReadAsStringAsync().Result);
                    }

                    var response = JsonConvert.DeserializeObject<PaymentResponse>(result.Content.ReadAsStringAsync().Result);

                    if (result.IsSuccessStatusCode)
                    {
                        //response.PaymentID = .Replace("\"", "");
                        return View("Avarda", response);
                    }                        
                }
            }
        return null;
        }
 

        private List<Item> ConvertShoppingCartItemToItem()
        {
            var itemList = new List<Item>();

            foreach (var item in _shoppingCart.GetShoppingCartItems())
            {
                itemList.Add(new Item {
                    Amount = (int)item.Product.Price,
                    Description = item.Product.Title
                });
            }
            return itemList;
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
        public IActionResult AuthorizeInvoice(InvoiceRequest request, Order order)
        {
            var total = _shoppingCart.GetShoppingCartTotal();
            order.OrderTotal = total;

            try
            {
                var response = _getCustomer.AuthorizeInvoice(request, order);

                if (total < response.Result.CreditLimit)//In tests the creditLimit is not fixed, which means that the if-statement is unnecessary but we will keep it for fun.
                {
                                                                                            //Save order and take customer to final page
                    order.OrderDetails = _orderRepository.CreateOrder(order);
                    _shoppingCart.ClearCart();                                              //Clears cart after "Complete order"
                    _emailHandler.SendEmail();                                              //Sends email to customer
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