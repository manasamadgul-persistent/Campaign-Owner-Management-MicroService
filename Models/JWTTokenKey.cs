using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMgmt.Models
{
    public interface IJWTTokenKey
    {
        public string JwtKey { get; set; }
    }
    public class JWTTokenKey:IJWTTokenKey
    {
        public string JwtKey { get; set; }
    }
}
