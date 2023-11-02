using AutoMapper;
using MeetUpCore.Entities;
using MeetUpCore.Interface;
using MeetUpCore.Models;
using MeetUpCore.ServiceCore.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace MeetUpCore.ServiceCore.MeetUps
{
    public class MeetUpService : IMeetUpService
    {   
        private readonly IMeetUpRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public MeetUpService(IMeetUpRepository repository, IMapper mapper, UserManager<User> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task CreateMeetUpAsync(MeetUpCreationModel meetCreationModel, long OrganizerID)
        {
            MeetUp MapedMeetUp = _mapper.Map<MeetUpCreationModel, MeetUp>(meetCreationModel);

            User Organizer = await _userManager.FindByIdAsync(OrganizerID.ToString());
            MapedMeetUp.Organizer = Organizer;

            await GetUsersByIDs(meetCreationModel.SpeakersIDs, MapedMeetUp);

            await _repository.CreateMeetUpAsync(MapedMeetUp);
        }

        public async Task DeleteMeetUpAsync(long MeetUpID, long UserID)
        {
            MeetUp MeetUpForDelete = await _repository.SearchMeetUpAsync(MeetUpID);

            if (MeetUpForDelete == null) 
            {
                throw new KeyNotFoundException("Meetup is not found"); 
            }

            if (MeetUpForDelete.OrganizerID != UserID) 
            {
                throw new ArgumentException("You are not the organizer of meetup."); 
            }

            await _repository.DeleteMeetUpAsync(MeetUpForDelete);
        }

        public async Task<List<MeetUpReturningModel>> GetAllAsync(PaginationSettingsModel paginationSettingsModel)
        {
            var AllMeetUps = await _repository.GetAllAsync(paginationSettingsModel);

            List<MeetUpReturningModel> MeetupModelsList = new List<MeetUpReturningModel>();
            foreach (var Meetup in AllMeetUps)
            {
                MeetUpReturningModel meetUpReturningModel = _mapper.Map<MeetUpReturningModel>(Meetup);
                MeetupModelsList.Add(meetUpReturningModel);
            }
            return MeetupModelsList;
        }

        public async Task<MeetUpReturningModel> SearchMetUpAsync(long ID)
        {
            var Meetup = await _repository.SearchMeetUpAsync(ID);
            MeetUpReturningModel meetUpReturningModel = _mapper.Map<MeetUpReturningModel>(Meetup);
            return meetUpReturningModel;
        }

        public async Task UpdateMeetUpAsync(MeetUpUpdateModel UpdateModel, long OrganizerID)
        {
            MeetUp MeetUpCheck = await _repository.SearchMeetUpAsync(UpdateModel.MeetUpID);

            if (MeetUpCheck == null)
            {
                throw new KeyNotFoundException("Meetup is not found");
            }

            if (MeetUpCheck.OrganizerID != OrganizerID)
            {
                throw new ArgumentException("You are not the organizer of meetup.");
            }

            _mapper.Map(UpdateModel, MeetUpCheck);

            await GetUsersByIDs(UpdateModel.AddSpeakersIDs, MeetUpCheck);

            await _repository.UpdateMeetUpAsync(MeetUpCheck);

        }
        private async Task GetUsersByIDs(List<long> Ids, MeetUp meetUp) 
        {
            if(!Ids.Any())
            {
                return;
            }

            if(meetUp.Speakers is null )
            {
                meetUp.Speakers = new();
            }
            else
            {
                meetUp.Speakers.Clear();
            }
            
            foreach (var SpeakerID in Ids)
            {
                var Speaker = await _userManager.FindByIdAsync(SpeakerID.ToString());
                
                if (Speaker == null) 
                {
                    throw new KeyNotFoundException($"There is no user with id {SpeakerID}");
                }

                meetUp.Speakers.Add(Speaker);
            }

            meetUp.Speakers = meetUp.Speakers.Distinct().ToList();
   
        }
    }
}
