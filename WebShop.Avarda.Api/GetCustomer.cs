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
    }
}
