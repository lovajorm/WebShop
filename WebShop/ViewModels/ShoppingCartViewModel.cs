using WebShop.Web.Interfaces;
using WebShop.Bo;
using WebShop.Web.Models;
using WebShop.Web.Repositories;

namespace WebShop.Web.ViewModels
{
    public class ShoppingCartViewModel
    {
            public ShoppingCart ShoppingCart { get; set; }
            public float ShoppingCartTotal { get; set; }       

            public Product Product { get; set; }
    }
}
