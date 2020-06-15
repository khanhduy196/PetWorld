using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;
using Authentication.Microservice.Domain.RequestDtos;
using Common.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Authentication.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAmazonCognitoIdentityProvider _cognitoIdentityProvider;
        public UserAuthenticationController(IAmazonCognitoIdentityProvider cognitoIdentityProvider, IConfiguration config)
        {
            _cognitoIdentityProvider = cognitoIdentityProvider;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserToCreateRequest userToCreateRequest)
        {
            try
            {           
                // Register the user using Cognito
                var signUpRequest = new SignUpRequest
                {
                    ClientId = _config.GetSection("AWS").GetSection("UserPoolClientId").Value,
                    Password = userToCreateRequest.Password,
                    Username = userToCreateRequest.Email

                };

                signUpRequest.UserAttributes.Add(new AttributeType
                {
                    Name = "phone_number",
                    Value = "+84" + userToCreateRequest.Phone
                });

                signUpRequest.UserAttributes.Add(new AttributeType
                {
                    Name = "name",
                    Value = userToCreateRequest.UserName
                });

                var reponse = await _cognitoIdentityProvider.SignUpAsync(signUpRequest);

                return Ok(new CommonResponse(ErrorCodes.SUCCESS));
            }
            catch (AmazonServiceException e)
            {             
                return BadRequest(new CommonResponse(ErrorCodes.ERROR, e.Message));
            }     
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserToLoginRequest userToLoginRequest)
        {
            try
            {
                CognitoUserPool userPool = new CognitoUserPool(_config.GetSection("AWS").GetSection("UserPoolId").Value, _config.GetSection("AWS").GetSection("UserPoolClientId").Value, _cognitoIdentityProvider);
                CognitoUser user = new CognitoUser(userToLoginRequest.Email, _config.GetSection("AWS").GetSection("UserPoolClientId").Value, userPool, _cognitoIdentityProvider);
                AuthFlowResponse context = await user.StartWithSrpAuthAsync(new InitiateSrpAuthRequest
                {
                    Password = userToLoginRequest.Password
                }).ConfigureAwait(false);

                return Ok(new CommonResponse(ErrorCodes.USER_NOT_CONFIRMED, string.Empty, context.AuthenticationResult.AccessToken));
            }
            catch (AmazonServiceException e)
            {
                switch(e.ErrorCode)
                {
                    case "UserNotConfirmedException":
                        return Ok(new CommonResponse(ErrorCodes.USER_NOT_CONFIRMED, e.Message));

                }
                return BadRequest(new CommonResponse(ErrorCodes.ERROR, e.Message));
            }

        }

        [HttpPost("confirm")]
        public async Task<IActionResult> Confirm(UserToConfirmRequest userToConfirmRequest)
        {
            try
            {
                ConfirmSignUpRequest confirmRequest = new ConfirmSignUpRequest()
                {
                    Username = userToConfirmRequest.Email,
                    ClientId = _config.GetSection("AWS").GetSection("UserPoolClientId").Value,
                    ConfirmationCode = userToConfirmRequest.Code
                };


                await _cognitoIdentityProvider.ConfirmSignUpAsync(confirmRequest);

                return Ok(new CommonResponse(ErrorCodes.SUCCESS));
            }
            catch (AmazonServiceException e)
            {
                return BadRequest(new CommonResponse(ErrorCodes.ERROR, e.Message));
            }
        }


        [HttpPost("resend")]
        public async Task<IActionResult> Resend(UserToResendRequest userToResendRequest)
        {
            try
            {
                AmazonCognitoIdentityProviderClient _client = new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), RegionEndpoint.USEast1);
                ResendConfirmationCodeRequest confirmRequest = new ResendConfirmationCodeRequest()
                {
                    Username = userToResendRequest.Email,
                    ClientId = _config.GetSection("AWS").GetSection("UserPoolClientId").Value
                };

                await _client.ResendConfirmationCodeAsync(confirmRequest);

                return Ok(new CommonResponse(ErrorCodes.SUCCESS));
            }
            catch (AmazonServiceException e)
            {
                return BadRequest(new CommonResponse(ErrorCodes.ERROR, e.Message));
            }
 
        }
    }
}