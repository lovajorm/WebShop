using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query;
using WebShop.Bo;
using WebShop.Dal.Repositories;
using WebShop.Web.ViewModels;

namespace WebShop.Web.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IIncludableQueryable<Product, Category> GetProducts();
    }
}