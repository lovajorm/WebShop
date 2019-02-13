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
        public IWebShopDbContext WebShopDbContext => Context as IWebShopDbContext;
        public OrderRepository(IWebShopDbContext context) : base(context){}
    }
}
