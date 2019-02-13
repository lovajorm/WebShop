using System.Collections.Generic;
using WebShop.Avarda.Api.Avarda;
using WebShop.Bo;

namespace WebShop.Web.Interfaces
{
    public interface IOrderRepository
    {
        List<OrderDetail> CreateOrder(Order order, PaymentStatus response);
    }
}
 