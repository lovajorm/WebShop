using System.Collections.Generic;


namespace WebShop.Avarda.Api.Avarda
{
    public class PaymentRequest
    {
        public float Price { get; set; }

        public List<Item> Items
        {
            get; set;
        }

    }
}
