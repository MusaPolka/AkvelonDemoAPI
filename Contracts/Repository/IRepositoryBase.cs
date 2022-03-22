using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repository
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FetchAll(bool trackChanges);
        IQueryable<T> FetchByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
