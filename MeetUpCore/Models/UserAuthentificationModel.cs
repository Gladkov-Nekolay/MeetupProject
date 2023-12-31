﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpCore.Models
{
    public class UserAuthentificationModel
    {
        [Required(ErrorMessage = "Email is requred")]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is requared")]
        public string Password { get; set; }
    }
}
