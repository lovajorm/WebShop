using System.Collections;
using System.Collections.Generic;
using WebShop.Bo;

namespace WebShop.Web.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}