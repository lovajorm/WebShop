using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Avarda.Api.Avarda;
using WebShop.Bo;

namespace WebShop.Dal.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order CreateOrder(PaymentStatus response);

        List<Item> GetItemsFromOrder(int orderId);
        void Update(Order order);
        List<OrderDetail> AddDetailsToOrder(List<ShoppingCartItem> shoppingCartItems, int orderId);
    }
}
