﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Microservice.Domain.RequestDtos
{
    public class UserToResendRequest
    {
        public string Email { get; set; }
    }
}
