using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CampaignMgmt.Models;

namespace CampaignMgmt.Data
{
    public class CampaignMgmtContext : DbContext
    {
        public CampaignMgmtContext (DbContextOptions<CampaignMgmtContext> options)
            : base(options)
        {
        }

        public DbSet<CampaignMgmt.Models.Owner> Owner { get; set; }
    }
}
