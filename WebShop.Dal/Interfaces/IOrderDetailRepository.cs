using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Bo;

namespace WebShop.Dal.Interfaces
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        OrderDetail ConvertProductToOrderDetail(Product product, int orderId);
    }
}
