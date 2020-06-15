using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Microservice.Application.RequestDtos
{
    public class UserToCreateRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
    }
}
