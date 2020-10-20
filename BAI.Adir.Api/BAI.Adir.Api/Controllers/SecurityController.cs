using BAI.Adir.Api.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using WebGrease.Css.Ast.Selectors;

namespace BAI.Adir.Api.Controllers
{
    public class SecurityController : ApiController
    {
        public HttpResponseMessage Post([FromBody] LoginDto login)
        {
            if(login.Username != "bai.admin" || login.Password != "1234")
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Credentials.");



            string secretKey = ConfigurationManager.AppSettings["JwtSecretKey"];



            var issuer = "http://codebiz.com.ph";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.NameId, "1"));
            permClaims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, "bai.admin"));

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            var result = new { token = jwt_token };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
