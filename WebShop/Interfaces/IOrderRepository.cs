using System.Collections.Generic;
using WebShop.Bo;

namespace WebShop.Web.Interfaces
{
    public interface IOrderRepository
    {
        List<OrderDetail> CreateOrder(Order order);
    }
}
 