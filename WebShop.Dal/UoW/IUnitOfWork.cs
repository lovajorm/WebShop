using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Dal;
using WebShop.Web.Interfaces;
using WebShop.Web.Models;

namespace WebShop.Web.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }

        ShoppingCart ShoppingCart { get; set; }
        
        int Complete();
    }
}
