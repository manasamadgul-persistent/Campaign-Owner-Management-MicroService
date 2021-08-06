using CampaignMgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMgmt.Repository
{
    public interface IOwnerRepository
    {
        IEnumerable<Owner> GetOwner();
        Owner GetOwnerById(string Id);
        void InsertOwner(Owner emp);
        void DeleteOwner(int Id);
        void UpdateOwner(string id,Owner owner);
        void Save();
    }
}
