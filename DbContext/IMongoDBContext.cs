using CampaignMgmt.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMgmt.DbContext
{
    public interface IMongoDBContext<TEntity> where TEntity : IEntity
    {
        IMongoCollection<TEntity> GetCollection<TEntity>(string name);
    }
}