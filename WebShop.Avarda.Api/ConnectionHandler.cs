﻿using System;
using WebShop.Avarda.Api.Avarda;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebShop.Bo;

namespace WebShop.Avarda.Api

{
    public class ConnectionHandler
    {
        private ConnectionDetails _connectionDetails;

        public ConnectionHandler()
        {
            _connectionDetails = new ConnectionDetails()
            { 
                Password = "123456",
                UserName = "Testpartner Sweden"
            };
        }
    
        public Customer GetCustomerInfo(string ssn)              //Method which gets customer information by using Ssn, see checkout.cshtml.
        {
            Customer response;                         //Initializes customer

            using (var handler = new WebRequestHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    var bytearray = Encoding.ASCII.GetBytes(_connectionDetails.ToString());

                    //sets authentication header.
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytearray));

                    //Sends a json-object and gets back result, converts result from json to c# which is "response"

                    var result = client.GetStringAsync(new Uri($"https://stage.avarda.org/WebShopApi/webshop/ssn/swe/{ssn}")).Result;
                    response = JsonConvert.DeserializeObject<Customer>(result);                  //Converts from json to c# class.
                }
            }
            return response;
        }

        public async Task<InvoiceResponse> AuthorizeInvoice(InvoiceRequest request, Order order)
        {
            using (var handler = new WebRequestHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    var bytearray = Encoding.ASCII.GetBytes(_connectionDetails.ToString());

                    //sets authentication header.
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytearray));

                    client.BaseAddress = new Uri($"https://stage.avarda.org/");

                    var total = order.OrderTotal;
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
                    
                    return response;
                }
            }
        }
    }
}
