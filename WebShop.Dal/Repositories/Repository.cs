using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

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
            return Context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return Context.Set<T>().Where(expression);
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
