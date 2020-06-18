using Amazon;
using Amazon.CognitoIdentityProvider;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Microservice.Infrastructure.Extensions
{
    public static class CognitoExtension
    {
        public static void CognitoConfigureServices(this IServiceCollection services)
        {
            var cognitoIdentityProvider = new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), RegionEndpoint.USEast2);
            services.AddSingleton<IAmazonCognitoIdentityProvider>(cognitoIdentityProvider);
        }
    }
}
