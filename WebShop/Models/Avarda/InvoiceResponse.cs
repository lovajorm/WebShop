using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Web.Models.Avarda
{
    public class InvoiceResponse
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public float CreditLimit { get; set; }
        public string AccountNumber { get; set; }
        public bool RequireDeliveryAuthorization { get; set; }
    }
}
