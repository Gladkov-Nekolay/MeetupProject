using MeetUpCore.Entities;
using MeetUpCore.Interface;
using MeetUpCore.Models;
using MeetUpInfrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpInfrastructure.Repositories.EFRepositories
{
    public class MeetUpEFRepository:IMeetUpRepository
    {
        private readonly MeetUpContext _dbcontext;
        public MeetUpEFRepository(MeetUpContext _context) 
        {
            _dbcontext = _context;
        }

        public async Task CreateMeetUpAsync(MeetUp meet)
        {
            await _dbcontext.meetUps.AddAsync(meet);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteMeetUpAsync(MeetUp meetUpForDeleting)
        {
            _dbcontext.meetUps.Remove(meetUpForDeleting);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<List<MeetUp>> GetAllAsync(PaginationSettingsModel paginationSettingsModel)
        {
            return await _dbcontext.meetUps.OrderBy(p=>p.ID)
                .Skip((paginationSettingsModel.PageNumber - 1)*paginationSettingsModel.PageSize)
                .Take(paginationSettingsModel.PageSize)
                .Include(p=>p.Speakers)
                .ToListAsync();
        }

        public async Task<MeetUp?> SearchMeetUpAsync(long ID)
        {
            return await _dbcontext.meetUps
                .AsNoTracking()
                .Include(x=>x.Speakers)
                .FirstOrDefaultAsync(x=>x.ID==ID);   
        }

        public async Task UpdateMeetUpAsync(MeetUp meet)
        {
            _dbcontext.meetUps.Update(meet);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<bool> IsExist(long ID) 
        {
            return await _dbcontext.meetUps.AnyAsync(x=>x.ID == ID);
        }
    }
}
