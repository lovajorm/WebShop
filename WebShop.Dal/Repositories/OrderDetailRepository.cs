using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Bo;
using WebShop.Dal.Interfaces;

namespace WebShop.Dal.Repositories
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public IWebShopDbContext WebShopDbContext => Context as IWebShopDbContext;
        public OrderDetailRepository(IWebShopDbContext context) : base(context) { }

        public OrderDetail ConvertProductToOrderDetail(Product product, int orderId)
        {
            var orderDetail = new OrderDetail()
            {
                Amount = 1,
                ProductID = product.ProductID,
                OrderId = orderId,
                Price = product.Price
            };
            return orderDetail;
        }
    }
}
