using MeetUpCore.Interface;
using MeetUpInfrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpInfrastructure.Repositories.EFRepositories
{
    public class UserEFRepository: IUserRepository
    {
        private readonly MeetUpContext _context;
        public UserEFRepository(MeetUpContext context)
        {
            _context = context;
        }
    }
}
