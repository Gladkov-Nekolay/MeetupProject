using MeetUpCore.Entities;
using MeetUpCore.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpInfrastructure.Context
{
    public class MeetUpContext:IdentityDbContext<User,IdentityRole<long>,long>
    {
        public MeetUpContext(DbContextOptions<MeetUpContext> options):base(options) { }

        public DbSet<MeetUp> meetUps { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole<long>> identityRoles = RoleNames.GetRolesList();

            builder.Entity<IdentityRole<long>>().HasData(identityRoles);

            builder.Entity<User>()
                .HasMany(x => x.OrganizerMeetUps)
                .WithOne(x => x.Organizer)
                .HasForeignKey(x => x.OrganizerID)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
