using WebShop.Bo;
using WebShop.Dal.Interfaces;
using WebShop.Dal.Repositories;

namespace WebShop.Dal.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IWebShopDbContext _context;
        public IProductRepository Product { get; }
        public ShoppingCart ShoppingCart { get; set; }
        public IOrderRepository Order { get; }

        public UnitOfWork(IWebShopDbContext context, IProductRepository productRepository, ShoppingCart shoppingCart)
        {
            _context = context;
            Product = new ProductRepository(context);
            ShoppingCart = shoppingCart;
            Order = new OrderRepository(context);
        }
        
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
