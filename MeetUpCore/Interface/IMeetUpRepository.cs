using MeetUpCore.Entities;
using MeetUpCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpCore.Interface
{
    public interface IMeetUpRepository
    {
        public Task CreateMeetUpAsync(MeetUp meet);
        public Task<MeetUp> SearchMeetUpAsync(long ID);
        public Task<List<MeetUp>> GetAllAsync(PaginationSettingsModel paginationSettingsModel);
        public Task UpdateMeetUpAsync(MeetUp meet);
        public Task DeleteMeetUpAsync(MeetUp MeetUpForDeleting);
        public Task<bool> IsExist(long ID);
    }
}
