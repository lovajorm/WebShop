using System.Collections.Generic;


namespace WebShop.Avarda.Api.Avarda
{
    public class PaymentRequest
    {

        //ShoppingCart cart = new ShoppingCart();

        //public decimal Price {
        //   get
        //    { 
        //        return this.Price;
        //    }

        //    set
        //    {
        //        ShoppingCart.Total = Price;
        //    }
        //}

        public float Price { get; set; }

        public List<Item> Items
        {
            get; set;
        }

    }
}
