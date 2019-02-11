using System.Collections.Generic;
using WebShop.Bo;
using WebShop.Dal.Repositories;

namespace WebShop.Web.Interfaces
{
    public interface IShoppingCartRepository: IRepository<ShoppingCartItem>
    {
        List<ShoppingCartItem> ShoppingCartItems { get; set; }

        List<ShoppingCartItem> GetShoppingCartItems();
        float GetShoppingCartTotal();
        void AddToCart(Product selectedProduct, int v);
        int RemoveFromCart(Product selectedProduct);
        List<OrderDetail> GetOrderDetailList(int id);
        void ClearCart();
    }
}