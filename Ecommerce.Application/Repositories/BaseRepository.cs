using Ecommerce.Application.Attributes;
using Ecommerce.Application.Entities;
using Ecommerce.Application.Entities.Interfaces;
using Ecommerce.Application.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using Pluralize.NET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Ecommerce.Application.Repositories
{
    public class BaseMongoRepository<T> : IRepository<T> where T : MongoEntity
    {
        private readonly string _collectionName;
        protected readonly IMongoDatabase Database;
        protected IMongoCollection<T> Collection { get; }

        public BaseMongoRepository(IMongoClient client, string databaseName) : this(client.GetDatabase(databaseName))
        {
        }

        public BaseMongoRepository(string connectionAddress, string databaseName) : this(
            new MongoClient(connectionAddress), databaseName)
        {
        }

        public BaseMongoRepository(IMongoDatabase database)
        {
            Database = database;
            var pluralizer = new Pluralizer();
            _collectionName = pluralizer.Pluralize(typeof(T).Name.Replace("Entity", ""));
            Collection = Database.GetCollection<T>(_collectionName);
            CreateIndex(AttributeProcessor.GetUniqueFields<T>(typeof(UniqueFieldAttribute)));
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Collection.AsQueryable();
        }

        public virtual T Insert(T entity)
        {
            Collection.InsertOne(entity);
            return entity;
        }

        protected virtual bool KeyCheck(object[] key, out string id)
        {
            if (key != null && key.Length > 0 && key[0] != null)
            {
                if (key[0] is string stringKey)
                {
                    id = stringKey;
                    return true;
                }
            }

            id = null;
            return false;
        }

        public virtual T GetByFieldFirst<TValue>(Expression<Func<T, TValue>> expression, TValue value)
        {
            return Collection.FindSync(Builders<T>.Filter.Eq(expression, value)).FirstOrDefault();
        }

        public virtual T GetById(params object[] key)
        {
            if (KeyCheck(key, out string id))
            {
                return Collection.FindSync(Builders<T>.Filter.Eq(x => x.Id, id)).FirstOrDefault();
            }

            return null;
        }

        public virtual void CreateIndex(IEnumerable<Expression<Func<T, object>>> uniqueFields)
        {
            var indexOptions = new CreateIndexOptions {Unique = true};
            foreach (var field in uniqueFields)
            {
                var indexModel = new CreateIndexModel<T>(Builders<T>.IndexKeys.Ascending(field), indexOptions);
                Collection.Indexes.CreateOne(indexModel);
            }
        }
    }

    public class AttributeProcessor
    {
        public static IEnumerable<Expression<Func<T, object>>> GetUniqueFields<T>(Type uniqueAttrType)
        {
            //var uniqueAttrType = typeof(UniqueFieldAttribute);
            var uniqueProps = typeof(T).GetProperties()
                .Where(x => x.GetCustomAttributes(true).Any(y => y.GetType() == uniqueAttrType));
            var genericType = typeof(T);
            var objectType = typeof(object);
            var parameter = Expression.Parameter(genericType, "x");

            foreach (var prop in uniqueProps)
            {
                Expression field = Expression.Property(parameter, prop);
                var lambda = Expression.Lambda<Func<T, object>>(field, parameter);
                yield return lambda;
            }
        }
    }
}