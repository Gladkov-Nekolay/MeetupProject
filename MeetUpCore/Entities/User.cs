using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpCore.Entities
{
    public class User : IdentityUser<long>
    {
        public List<MeetUp>? OrganizerMeetUps { get; set; } 
        public List<MeetUp>? SpeakerMeetUps { get; set; }
    }
}
