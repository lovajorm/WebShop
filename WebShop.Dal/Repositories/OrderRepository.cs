using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebShop.Bo;
using WebShop.Dal.Interfaces;

namespace WebShop.Dal.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {

        public OrderRepository(IWebShopDbContext context) : base(context)
        {

            //List<Order> GetOrders()
            //{
            //    return context.Orders.ToList();
            //}

            //List<Order> GetOrder(int id)
            //{
            //    return context.Orders.Where(x => x.OrderId == id).ToList();
            //}
        }
        public IWebShopDbContext WebShopDbContext => Context as IWebShopDbContext;


    }
}
