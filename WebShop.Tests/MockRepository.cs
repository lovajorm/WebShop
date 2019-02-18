using System.Collections.Generic;
using System.Linq;
using WebShop.Dal.Interfaces;

namespace WebShop.Tests
{
    public class MockRepository<T> : IRepository<T> where T : class
    {
        public List<T> _context;
        public MockRepository(List<T> context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public IEnumerable<T> Find(System.Linq.Expressions.Expression<System.Func<T, bool>> expression)
        {
            throw new System.NotImplementedException();
        }

        public T Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.AsEnumerable();
        }

        public void Remove(T entity)
        {
            _context.Remove(entity);
        }
    }
}