using System;
using WebShop.Dal.Interfaces;

namespace WebShop.Dal.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }

        ShoppingCart ShoppingCart { get; set; }
        
        int Complete();
    }
}
