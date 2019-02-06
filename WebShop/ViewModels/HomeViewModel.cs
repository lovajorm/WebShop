using System.Collections.Generic;
using WebShop.Bo;

namespace WebShop.Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> PopularProducts { get; set; }
    }
}
