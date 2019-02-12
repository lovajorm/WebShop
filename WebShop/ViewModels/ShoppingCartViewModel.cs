using WebShop.Bo;
using WebShop.Web.Models;

namespace WebShop.Web.ViewModels
{
    public class ShoppingCartViewModel
    {
            public ShoppingCart ShoppingCart { get; set; }
            public float ShoppingCartTotal { get; set; }       

            public Product Product { get; set; }
    }
}
