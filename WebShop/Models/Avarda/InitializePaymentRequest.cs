using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Web.Models.Avarda
{
    public class InitializePaymentRequest
    {
        public decimal Price { get; set; }
        public string Mail { get; set; }
        public string Description { get; set; }
        public List<Items> Items { get; set; }
    }
}
