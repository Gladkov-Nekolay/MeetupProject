using AutoMapper;
using MeetUpCore.Entities;
using MeetUpCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpCore.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserCreationModel>()
               .ForMember(u => u.Name, option => option.MapFrom(src => src.UserName))
               .ReverseMap();
            CreateMap<User, UserReturningModel>();
        }
    }
}
