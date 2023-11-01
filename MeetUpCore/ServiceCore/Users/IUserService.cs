using MeetUpCore.Entities;
using MeetUpCore.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpCore.ServiceCore.Users
{
    public interface IUserManager
    {
        public JwtSecurityToken GetNewToken(List<Claim> claims);
        public List<Claim> GetClaims(User user, IList<string> UserRoles);
        public Task RegisterAsync(UserCreationModel model);
        public Task<string> LoginUserAsync(UserAuthentificationModel model);
    }
}
