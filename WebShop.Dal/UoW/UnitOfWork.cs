using Microsoft.EntityFrameworkCore;
using WebShop.Bo;
using WebShop.Dal.Interfaces;
using WebShop.Dal.Repositories;

namespace WebShop.Dal.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IWebShopDbContext _context;
        public IProductRepository Product { get; }
        public IOrderRepository Order { get; }
        public DbSet<ShoppingCartItem> ShoppingCart { get; }
        public IOrderDetailRepository OrderDetail { get; }

        public UnitOfWork(WebShopDbContext context)
        {
            _context = context;
            Product = new ProductRepository(context);
            Order = new OrderRepository(context);
            ShoppingCart = context.ShoppingCartItems;
            OrderDetail = new OrderDetailRepository(context);
        }

        public int Complete()
        {
            return _context.Complete();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
