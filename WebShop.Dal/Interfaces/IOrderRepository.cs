using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Avarda.Api.Avarda;
using WebShop.Bo;

namespace WebShop.Dal.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        List<OrderDetail> CreateOrder(Order order, PaymentStatus response, List<ShoppingCartItem> items);

        List<Item> GetItemsFromOrder(int orderId);
        void Update(Order order);
    }
}
