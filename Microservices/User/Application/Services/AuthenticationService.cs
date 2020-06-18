using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Microservice.Application.RequestDtos;

namespace User.Microservice.Application.Services
{
    public interface IAuthenticationService
    {
        public void Register(UserToCreateRequest userToCreateRequest);
    }
    public class AuthenticationService : IAuthenticationService
    {
        public void Register(UserToCreateRequest userToCreateRequest)
        {

        }
    }
}
