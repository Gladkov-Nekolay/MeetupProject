using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpCore.Models
{
    public class UserCreationModel
    {
        [Required(ErrorMessage = "Email is requred")]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Name is requared")]
        [MaxLength(100, ErrorMessage = "Name should be shorter than 100 symbols")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password is requared")]
        [MinLength(4, ErrorMessage = "Password should be longer than 4 symbols")]
        public string Password { get; set; }
    }
}
