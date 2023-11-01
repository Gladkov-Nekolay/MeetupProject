using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpCore.Roles
{
    public class RoleNames
    {
        public const string USER = "user";
        public const string ADMIN = "admin";

        private static readonly List<IdentityRole<long>> RolesList = new List<IdentityRole<long>>()
        {
            new IdentityRole<long>(USER) { Id = 1,NormalizedName="USER"},
            new IdentityRole<long>(ADMIN) { Id = 2,NormalizedName="ADMIN"}
        };

        public static List<IdentityRole<long>> GetRolesList() 
        {
            return RolesList;
        }
    }
}
