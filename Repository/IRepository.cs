using CampaignMgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMgmt.Repository
{
    public interface IRepository<TEntity> where TEntity:IEntity
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(string id);
        Task Add(TEntity obj);
        void Update(TEntity obj);
        void Delete(string id);
    }
}
