using CampaignMgmt.DbContext;
using CampaignMgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMgmt.Repository
{
    public class OwnerRepository: GenericRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(IMongoDBContext<Owner> context) : base(context)
        {

        }
    }
}
