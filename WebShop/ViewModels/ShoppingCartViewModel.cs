using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Web.Models;

namespace WebShop.Web.ViewModels
{
    public class ShoppingCartViewModel
    {
            public ShoppingCart ShoppingCart { get; set; }
            public float ShoppingCartTotal { get; set; }       
    }
}
