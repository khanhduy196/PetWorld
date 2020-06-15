using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;
using Cqrs;
using Microsoft.AspNetCore.Mvc;

using User.Microservice.Application.Commands.CreatePet;
using User.Microservice.Application.RequestDtos;

namespace User.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IBaseCqrs _cqrs;
        public PetController(IBaseCqrs crqs)
        {
            this._cqrs = crqs;
        }
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var command = new CreatedPetCommand
            {
                Name = "Meo Con"
            };
            await this._cqrs.SendCommand(command);

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserToCreateRequest userToCreateRequest)
        {
            var _client = new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), RegionEndpoint.USEast1);
            // Register the user using Cognito
            var signUpRequest = new SignUpRequest
            {
                ClientId = "4s8cbl7ptf75hp2tbqc14coib7",
                Password = "Lalal196",
                Username = "khanhduyuit+6@gmail.com",

            };

            signUpRequest.UserAttributes.Add(new AttributeType { 
                Name = "phone_number",
                Value = "+84765998291"
            });

            signUpRequest.UserAttributes.Add(new AttributeType
            {
                Name = "name",
                Value = "NguyenKhanhDuy"
            });

            var reponse = await _client.SignUpAsync(signUpRequest);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            AmazonCognitoIdentityProviderClient _client = new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), RegionEndpoint.USEast1);
            CognitoUserPool userPool = new CognitoUserPool("us-east-1_EttBoTOfx", "4s8cbl7ptf75hp2tbqc14coib7", _client);
            CognitoUser user = new CognitoUser("khanhduyuit+8@gmail.com", "4s8cbl7ptf75hp2tbqc14coib7", userPool, _client);
            AuthFlowResponse context = await user.StartWithSrpAuthAsync(new InitiateSrpAuthRequest
            {
                Password = "Lalala196"
            }).ConfigureAwait(false);


            return Ok(context.AuthenticationResult.AccessToken);
        }
    }
}