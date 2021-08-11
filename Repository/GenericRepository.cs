using CampaignMgmt.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMgmt.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity:IEntity
    {
        private readonly IMongoCollection<TEntity> _dbCollection;
        protected readonly string key;
        public GenericRepository(IOwnerDBSettings settings,IJWTTokenKey tokenKey)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _dbCollection = database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
            key = tokenKey.JwtKey;
        }
        private protected string GetCollectionName(Type type)
        {
            return ((BsonCollectionAttribute)type.GetCustomAttributes(
                typeof(BsonCollectionAttribute),
                true)
            .FirstOrDefault())?.CollectionName;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var all = await _dbCollection.FindAsync(Builders<TEntity>.Filter.Empty);
            return await all.ToListAsync();
        }

        public async Task<TEntity> Get(string id)
        {
            var objectId = new ObjectId(id);
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task Add(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
            }
            await _dbCollection.InsertOneAsync(obj);
        }
        public void Update(TEntity obj)
        {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, obj.Id);
            _dbCollection.FindOneAndReplace(filter, obj);
        }

        public void Delete(string id)
        {

            var objectId = new ObjectId(id);
            _dbCollection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", objectId));

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
