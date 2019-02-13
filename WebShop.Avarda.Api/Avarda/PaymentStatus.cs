using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Avarda.Api.Avarda
{
    public class PaymentStatus
    {
        public string PurchaseId { get; set; }
        public int State { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }  
    }
}
