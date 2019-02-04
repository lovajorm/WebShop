using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebShop.Web.Models.Avarda
{
    public class RequestHandler
    {
        public InitializePaymentResponse GetPaymentResponse(ConnectionDetails auth)
        {
            var createRequest = CreateRequest();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(auth.ToBytes));
                try
                {
                    HttpResponseMessage httpRequest = client.PostAsJsonAsync(auth.Url, createRequest).Result;
                    var content = httpRequest.Content.ReadAsStringAsync().Result;

                    if (httpRequest.IsSuccessStatusCode)
                    {
                        return (new InitializePaymentResponse { purchaseId = content });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return null;
        }

        private object CreateRequest()
        {
            var request = new InitializePaymentRequest
            {
                Description = "New Description",
                Mail = "mail@mail.mail",
                Price = 100,
                Items = new List<Items>
                {
                    new Items
                    {
                        Amount = 100,
                        Description = "New Item"
                    }
                }
            };
            return request;
        }
    }
}
