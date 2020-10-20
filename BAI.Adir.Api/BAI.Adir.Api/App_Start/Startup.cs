using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;

[assembly:OwinStartup(typeof(BAI.Adir.Api.App_Start.Startup))]
namespace BAI.Adir.Api.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string secretKey = ConfigurationManager.AppSettings["JwtSecretKey"];
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "http://codebiz.com.ph", //some string, normally web url,  
                        ValidAudience = "http://codebiz.com.ph",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    }
                });
        }
    }
}