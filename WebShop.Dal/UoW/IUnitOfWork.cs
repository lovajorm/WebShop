using System;
using WebShop.Dal.Interfaces;

namespace WebShop.Dal.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        IOrderRepository Order { get; }

        ShoppingCart ShoppingCart { get; set; }
        
        int Complete();
    }
}
