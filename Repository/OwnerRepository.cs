using CampaignMgmt.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMgmt.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IMongoCollection<Owner> _owners;
        public OwnerRepository(IOwnerDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _owners = database.GetCollection<Owner>(settings.OwnersCollectionName);

        }
        public void DeleteOwner(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Owner> GetOwner()
        {
            var owners = _owners.Find(emp => true).ToList();
            return owners;
        }

        public Owner GetOwnerById(string Id)
        {
            ObjectId oId = new ObjectId(Id);
            Owner owner = _owners.Find(e => e.Id
            == oId).FirstOrDefault();
            return owner;
        }

        public void InsertOwner(Owner owner)
        {
            _owners.InsertOne(owner);            
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateOwner(string id,Owner owner)
        {
            owner.Id = new ObjectId(id);
            var filter = Builders<Owner>.
            Filter.Eq("Id", owner.Id);
            var updateDef = Builders<Owner>.Update.Set("Channels", owner.Channels);
            updateDef = updateDef.Set("Contact", owner.Contact);
            var result = _owners.UpdateOne(filter, updateDef);
            //throw new NotImplementedException();
        }
    }
}
