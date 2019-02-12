using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebShop.Bo;

namespace WebShop.Dal.Repositories
{
    public class OrderRepository : Repository<Order>, IRepository<Order>
    {
        public OrderRepository(IWebShopDbContext context) : base(context)
        {
            List<Order> GetOrders()
            {
                return context.Orders.ToList();
            }

            List<Order> GetOrder(int id)
            {
                return context.Orders.Where(x => x.OrderId == id).ToList();
            }
        }
    }
}
