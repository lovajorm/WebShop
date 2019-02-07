using System.Collections.Generic;
using WebShop.Bo;

namespace WebShop.Web.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public string CurrentCategory { get; set; }
    }
}
