using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Entities.Interfaces
{
    public interface IEntity<T> : IEntity
    {
        string Id { get; set; }
    }
}
