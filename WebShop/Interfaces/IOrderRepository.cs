﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Bo;

namespace WebShop.Web.Interfaces
{
    public interface IOrderRepository
    {
        List<OrderDetail> CreateOrder(Order order);
    }
}
 