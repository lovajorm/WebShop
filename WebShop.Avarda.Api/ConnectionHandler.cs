using System;
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
                Password = "test1",
                UserName = "TestSweden1"
            };
        }

        public void PurchaseOrder(PurchaseOrderRequest request)
        {
            using (var handler = new WebRequestHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    var bytearray = Encoding.ASCII.GetBytes(_connectionDetails.ToString());  

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytearray));

                    client.BaseAddress = new Uri($"https://stage.avarda.org/");

                    var body = JsonConvert.SerializeObject(request);
                    var requestBody = new StringContent(body, Encoding.UTF8, "application/json");
                    var result = client.PostAsync("checkout2/CheckOut2Api/Commerce/PurchaseOrder", requestBody).Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        throw new Exception(result.Content.ReadAsStringAsync().Result);
                    }

                    //if (result.IsSuccessStatusCode)
                    //{
                    //    response = JsonConvert.DeserializeObject<PaymentStatus>(result.Content.ReadAsStringAsync().Result);
                    //}
                }
            }
        }

        public PaymentStatus GetPaymentStatus(string paymentId)
        {
            PaymentStatus response = null;
            var request = new PaymentResponse()
            {
                PurchaseId = paymentId
            };
            using (var handler = new WebRequestHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    var bytearray = Encoding.ASCII.GetBytes(_connectionDetails.ToString());               //Authentication

                    //sets authentication header.
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytearray));

                    client.BaseAddress = new Uri($"https://stage.avarda.org/");

                    //Serializes the class to json
                    
                    var body = JsonConvert.SerializeObject(request);
                    var requestBody = new StringContent(body, Encoding.UTF8, "application/json");
                    var result = client.PostAsync("CheckOut2/CheckOut2Api/GetPaymentStatus", requestBody).Result;                                          //Calling Avarda API and sending the json

                    if (!result.IsSuccessStatusCode)
                    {
                        throw new Exception(result.Content.ReadAsStringAsync().Result);
                    }

                    //response.PurchaseId = result.Content.ReadAsStringAsync().Result;

                    if (result.IsSuccessStatusCode)
                    {
                        response = JsonConvert.DeserializeObject<PaymentStatus>(result.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            return response;
        }

        public PaymentResponse InitializePayment(PaymentRequest request)
        {
            //var request = new PaymentRequest();
            var response = new PaymentResponse();

            using (var handler = new WebRequestHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    var bytearray = Encoding.ASCII.GetBytes(_connectionDetails.ToString());               //Authentication

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

                    response.PurchaseId = result.Content.ReadAsStringAsync().Result;

                    if (result.IsSuccessStatusCode)
                    {
                        response.PurchaseId = response.PurchaseId.Replace("\"", string.Empty);
                        //return View("Avarda", response);
                    }
                }
            }
            return response;
        }
    }
}

