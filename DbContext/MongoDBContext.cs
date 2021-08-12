using CampaignMgmt.Models;
using CampaignMgmt.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMgmt.DbContext
{
    public class MongoDBContext<TEntity> : IMongoDBContext<TEntity> where TEntity : IEntity
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public MongoDBContext(IOwnerDBSettings settings)
        {
            _mongoClient = new MongoClient(settings.ConnectionString);
            _db = _mongoClient.GetDatabase(settings.DatabaseName);

            //var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            //_dbCollection = (IMongoDatabase)database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
        }

       
        public IMongoCollection<TEntity> GetCollection<TEntity>(string name)
        {
            return _db.GetCollection<TEntity>(name);
        }

    }
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BsonCollectionAttribute : Attribute
    {
        public string CollectionName { get; }

        public BsonCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}
