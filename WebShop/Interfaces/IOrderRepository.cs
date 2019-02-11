using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Bo;
using WebShop.Dal.Repositories;

namespace WebShop.Web.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        List<OrderDetail> CreateOrder(Order order);
    }
}
 