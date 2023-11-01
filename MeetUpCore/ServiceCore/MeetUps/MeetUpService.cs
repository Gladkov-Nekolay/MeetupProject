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

            MapedMeetUp.Speakers = await GetUsersByIDs(meetCreationModel.SpeakersIDs);

            await _repository.CreateMeetUpAsync(MapedMeetUp);
        }

        public async Task DeleteMeetUpAsync(long MeetUpID, long UserID)
        {
            MeetUp MeetUpForDelete = await _repository.SearchMetUpAsync(MeetUpID);

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

        public async Task<List<MeetUp>> GetAllAsync(PaginationSettingsModel paginationSettingsModel)
        {
            return await _repository.GetAllAsync(paginationSettingsModel);
        }

        public async Task<MeetUp> SearchMetUpAsync(long ID)
        {
            return await _repository.SearchMetUpAsync(ID);
        }

        public async Task UpdateMeetUpAsync(MeetUpUpdateModel UpdateModel, long OrganizerID)
        {
            MeetUp MeetUpCheck = await _repository.SearchMetUpAsync(UpdateModel.MeetUpID);

            if (MeetUpCheck == null)
            {
                throw new KeyNotFoundException("Meetup is not found");
            }

            if (MeetUpCheck.OrganizerID != OrganizerID)
            {
                throw new ArgumentException("You are not the organizer of meetup.");
            }

            //MeetUpCheck = _mapper.Map<MeetUp>(UpdateModel);
            _mapper.Map(UpdateModel, MeetUpCheck);

            MeetUpCheck.Speakers = await GetUsersByIDs(UpdateModel.SpeakersIDs);
            
            //MeetUpCheck.OrganizerID = OrganizerID;

            await _repository.UpdateMeetUpAsync(MeetUpCheck);

        }
        private async Task<List<User>> GetUsersByIDs(List<long> Ids) 
        {
            List<User> Speakers = new List<User>();

            foreach (var SpeakerID in Ids)
            {
                var Speaker = await _userManager.FindByIdAsync(SpeakerID.ToString());
                
                if (Speaker == null) 
                {
                    throw new KeyNotFoundException($"There is no user with id {SpeakerID}");
                }

                Speakers.Add(Speaker);
            }

            return Speakers;
        }
    }
}
