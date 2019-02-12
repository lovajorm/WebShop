using System;
using WebShop.Avarda.Api.Avarda;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

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

        

        public PaymentResponse InitializePayment(PaymentRequest request)
        {
            var response = new PaymentResponse();

            using (var handler = new WebRequestHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    var bytearray = Encoding.ASCII.GetBytes("TestSweden1:test1");               //Authentication

                    //sets authentication header.
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytearray));

                    client.BaseAddress = new Uri($"https://stage.avarda.org/");
                    
                    //Serializes the class to json
                    var body = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                    var result = client.PostAsync("CheckOut2/CheckOut2Api/InitializePayment", body).Result;                                          //Calling Avarda API and sending the json

                    if (!result.IsSuccessStatusCode)
                    {
                        throw new Exception(result.Content.ReadAsStringAsync().Result);
                    }

                    response.PaymentID = result.Content.ReadAsStringAsync().Result;

                    if (result.IsSuccessStatusCode)
                    {
                        response.PaymentID = response.PaymentID.Replace("\"", string.Empty);
                        //return View("Avarda", response);
                    }
                }
            }
            return response;
        }


    }
}

