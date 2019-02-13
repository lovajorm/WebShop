using System;
using Microsoft.EntityFrameworkCore;
using WebShop.Bo;
using WebShop.Dal.Interfaces;

namespace WebShop.Dal.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        IOrderRepository Order { get; }
        DbSet<ShoppingCartItem> ShoppingCart { get; }

        int Complete();
    }
}
