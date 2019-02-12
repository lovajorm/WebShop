using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebShop.Dal.Interfaces;

namespace WebShop.Dal.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IWebShopDbContext Context;  //derived class access because inheritance

        public Repository(IWebShopDbContext context)
        {
            Context = context;
        }
        public T Get(int id)
        {
            return Context.Get<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Context.GetAll<T>();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return Context.Find(expression);
        }

        public void Add(T entity)
        {
            Context.Add(entity);
        }

        public void Remove(T entity)
        {
            Context.Remove(entity);
        }
    }
}
