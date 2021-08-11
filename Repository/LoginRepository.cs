using CampaignMgmt.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CampaignMgmt.Repository
{
    public class LoginRepository : GenericRepository<User>, ILoginRepository
    {
        public LoginRepository(IOwnerDBSettings ownerDBSettings, IJWTTokenKey jWTTokenKey) : base(ownerDBSettings, jWTTokenKey)
        {
        }

        public string Authenticate(string UserName, string Password)
        {
            List<User> users = this.GetAll().Result.ToList();

            var user = users.Find(x => x.UserName == UserName && x.Password == Password);
            
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(this.key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, UserName),
                    }),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
