using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpCore.Models
{
    public class MeetUpUpdateModel
    {
        [Required]
        public long MeetUpID { get; set; }
        public string? Name { set; get; }
        public string? Theme { set; get; }
        public string? Description { set; get; }
        public string? Plan { set; get; }
        public List<long> SpeakersIDs { set; get; } = new List<long>();
        [Required]
        public DateTime MeetUpTime { set; get; }
        public string? Place { set; get; }
    }
}
