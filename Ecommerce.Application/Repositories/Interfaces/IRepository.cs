using Ecommerce.Application.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Repositories.Interfaces
{

    public interface IRepository<T> where T : IEntity
    {
        public IEnumerable<T> GetAll();
        T Insert(T entity);
    }
}
