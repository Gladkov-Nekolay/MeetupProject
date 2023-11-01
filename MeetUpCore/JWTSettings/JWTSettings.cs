﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpCore.JWTSetting
{
    public class JWTSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int LifetimeHours { get; set; }
        public SymmetricSecurityKey SecretKey { get; set; }
    }
}
