using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultation.Microservice.Domain.Logics
{
    public class UserLogic
    {
        public static bool IsCurrentUser(string userId)
        {
            return userId == "accessControl.UserId";
        }
    }
}
