using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultation.Microservice.Domain.Services
{
    public class UserService
    {
        public string CreateUserService()
        {
            if (Logics.UserLogic.IsCurrentUser("a"))
            {
                return null;
            }
            return "userId";
        }
    }
}
