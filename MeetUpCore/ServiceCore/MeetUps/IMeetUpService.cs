using MeetUpCore.Entities;
using MeetUpCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpCore.ServiceCore.MeetUps
{
    public interface IMeetUpService
    {
        public Task CreateMeetUpAsync(MeetUpCreationModel meet, long OrganizerID);
        public Task<MeetUpReturningModel> SearchMetUpAsync(long ID);
        public Task<List<MeetUpReturningModel>> GetAllAsync(PaginationSettingsModel paginationSettingsModel);
        public Task UpdateMeetUpAsync(MeetUpUpdateModel meet, long OrganizerID);
        public Task DeleteMeetUpAsync(long MeetUpID, long OrganizerID);
    }
}
