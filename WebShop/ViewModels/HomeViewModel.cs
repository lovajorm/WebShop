using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Bo;

namespace WebShop.Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> PopularProducts { get; set; }
    }
}
