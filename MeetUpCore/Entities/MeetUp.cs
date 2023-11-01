using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpCore.Entities
{
    public class MeetUp
    {
        [Key]
        public long ID { get; set; }
        public string? Name { set; get; }
        public string? Theme { set; get; }
        public string? Description { set; get; }
        public string? Plan { set; get; }

        [Required]
        public long? OrganizerID { set; get; }

        [InverseProperty(nameof(User.OrganizerMeetUps))]
        public User? Organizer { set; get; }

        [InverseProperty(nameof(User.SpeakerMeetUps))]
        public List<User>? Speakers { set; get; }

        public DateTime MeetUpTime { set; get; }
        public string? Place { set; get; }

    }
}
