using System;
using Microsoft.AspNetCore.Mvc;
using WebShop.Avarda.Api.Avarda;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace WebShop.Avarda.Api

{
    public class GetCustomer
    {
        public Customer GetCustomerInfo(string ssn)              //Method which gets customer information by using Ssn, see checkout.cshtml.
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

                    var result = client.GetStringAsync(new Uri($"https://stage.avarda.org/WebShopApi/webshop/ssn/swe/{ssn}")).Result;
                    response = JsonConvert.DeserializeObject<Customer>(result);                  //Converts from json to c# class.
                }
            }
            return response;
        }

        public  AuthorizeInvoice()
        {
            using (var handler = new WebRequestHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    var bytearray = Encoding.ASCII.GetBytes("Testpartner Sweden:123456");

                    //sets authentication header.
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytearray));

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

                }
            }
            //return View("Checkout");
        }

    }
}
