using Ecommerce.Application.Entities.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ecommerce.Application.Entities
{
    public abstract class MongoEntity : IEntity<ObjectId>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public object IdBase
        {
            get => Id;
            set => Id = (string) value;
        }
    }
}