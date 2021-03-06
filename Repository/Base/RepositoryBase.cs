using Contracts.Repository;
using DataAccessLayer.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// Repository base class that provides us CRUD operations, following Repository Pattern logic
    /// 
    /// Tracking changes improve our read-only queries. Which makes our app perform a lot faster
    /// </summary>
    /// <typeparam name="T">Any class that wants to use our CRUD operations</typeparam>
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AkvelonDemoAPIContext _context;

        public RepositoryBase(AkvelonDemoAPIContext context)
        {
            _context = context;
        }

        public IQueryable<T> FetchAll(bool trackChanges)
        {
            if (trackChanges == false)
            {
                return _context.Set<T>().AsNoTracking();
            }

            return _context.Set<T>();
        }

        public IQueryable<T> FetchByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            if (trackChanges == false)
            {
                return _context.Set<T>().Where(expression).AsNoTracking();
            }

            return _context.Set<T>().Where(expression);
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
