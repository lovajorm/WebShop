using WebShop.Web.Models;
using WebShop.Bo;

namespace WebShop.Web.ViewModels
{
    public class ShoppingCartViewModel
    {
            public ShoppingCart ShoppingCart { get; set; }
            public float ShoppingCartTotal { get; set; }       

            public Product Product { get; set; }
    }
}
