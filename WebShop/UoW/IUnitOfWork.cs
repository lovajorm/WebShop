using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Web.Interfaces;

namespace WebShop.Web.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        int Complete();
    }
}
