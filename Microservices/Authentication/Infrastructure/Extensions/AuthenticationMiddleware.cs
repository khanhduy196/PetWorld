using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Microservice.Infrastructure.Extensions
{
    public static class AuthenticationExtension
    {
        private static byte[] FromBase64Url(string base64Url)
        {
            string padded = base64Url.Length % 4 == 0
                ? base64Url : base64Url + "====".Substring(base64Url.Length % 4);
            string base64 = padded.Replace("_", "/")
                                    .Replace("-", "+");
            return Convert.FromBase64String(base64);
        }

        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(
              new RSAParameters()
              {
                  Modulus = FromBase64Url("gb-BTybjkQmFpnk-d6DnxbWks4tfkXYHt6lcagCI-BTy38eStP-ckIrTy9u_SH860_G9KpR-WUPwG-v4JwarjdgQ2wdi6DTSVWilA68YQExfyVvKbudHgTjHhcLnj1nVSW9qLYnQOXpjrQxeSQVF-sUyeWLQMrK9GYC-YAGHMNOLJhtm1RiEfmJZ776E3BtTdUGXX1-QJVRtZfx2s3hjWO2IkxXesjp5Uqti9v4jW9oTf4jRqIhuJb4yaFVqVxOOGNHt4nW_UeOJHFS3kG_XSfQGPximgBGS9eqw3FN8AGSTuhl7Di4cNm_28rqMoDe0AW7-Lu0Rg-Vt_LZcs43KzQ"),
                  Exponent = FromBase64Url("AQAB")
              });


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                        RequireExpirationTime = true,
                                        RequireSignedTokens = true,
                                        ValidateAudience = false,
                                        //ValidAudience = "1tnkan81k0sv47jpasi3q1eq8i",
                                        ValidateIssuer = true,
                                        ValidIssuer = $"https://cognito-idp.us-east-2.amazonaws.com/us-east-2_JFmagphKZ",
                                        ValidateLifetime = false,
                                        IssuerSigningKey = new RsaSecurityKey(rsa)
                                };
                            });
            return services;
        }
    }
}
