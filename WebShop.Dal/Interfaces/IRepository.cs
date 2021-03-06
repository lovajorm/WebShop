﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebShop.Dal.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression); 
        void Add(T entity);
        void Remove(T entity);
    }
}
