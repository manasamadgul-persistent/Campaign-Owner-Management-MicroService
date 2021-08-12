using CampaignMgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMgmt.Repository
{
    public interface ILoginRepository:IRepository<User>
    {
        public string Authenticate(string UserName, string Password, string key);
    }
}
