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
    public class MeetUpProfile:Profile
    {
        public MeetUpProfile() 
        {
            CreateMap<MeetUpCreationModel, MeetUp>().ForMember(x => x.Name, option => option.MapFrom(src => src.Name))
                                                    .ForMember(x => x.Theme, option => option.MapFrom(src => src.Theme))
                                                    .ForMember(x => x.MeetUpTime, option => option.MapFrom(src => src.MeetUpTime))
                                                    .ForMember(x => x.Description, option => option.MapFrom(src => src.Description))
                                                    .ForMember(x => x.Place, option => option.MapFrom(src => src.Place))
                                                    .ForMember(x => x.Plan, option => option.MapFrom(src => src.Plan));

            CreateMap<MeetUpUpdateModel, MeetUp>().ForMember(x => x.Name, option => option.MapFrom(src => src.Name))
                                                    .ForMember(x => x.Theme, option => option.MapFrom(src => src.Theme))
                                                    .ForMember(x => x.MeetUpTime, option => option.MapFrom(src => src.MeetUpTime))
                                                    .ForMember(x => x.Description, option => option.MapFrom(src => src.Description))
                                                    .ForMember(x => x.Place, option => option.MapFrom(src => src.Place))
                                                    .ForMember(x => x.Plan, option => option.MapFrom(src => src.Plan))
                                                    .ForMember(x => x.ID, option => option.MapFrom(src => src.MeetUpID))
                                                    .ForMember(x => x.OrganizerID, option => option.Ignore())
                                                    .ForMember(x => x.Organizer, option => option.Ignore());

        }
    }
}
