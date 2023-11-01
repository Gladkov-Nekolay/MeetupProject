using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpCore.Models
{
    public class PaginationSettingsModel
    {
        [Required(ErrorMessage = "Page Number is requared")]
        [Range(1, int.MaxValue, ErrorMessage = "Page number should be greater or equals 1")]
        public int PageNumber { get; set; } = 1;
        [Required(ErrorMessage = "Page size is requared")]
        [Range(1, 100, ErrorMessage = "Page should have from 1 to 100 records")]
        public byte PageSize { get; set; } = 10;
    }
}
