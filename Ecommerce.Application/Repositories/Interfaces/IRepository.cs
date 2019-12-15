using Ecommerce.Application.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ecommerce.Application.Repositories.Interfaces
{

    public interface IRepository<T> where T : IEntity
    {
        public IEnumerable<T> GetAll();
        T Insert(T entity);
        T GetById(params object[] key);
        T GetByFieldFirst<TValue>(Expression<Func<T, TValue>> expression, TValue value);
    }
}
