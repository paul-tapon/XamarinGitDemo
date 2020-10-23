using BAI.Adir.Api.Domain.Context;
using BAI.Adir.Api.Domain.Model;
using BAI.Adir.Api.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BAI.Adir.Api.Controllers
{
    public class UserController : ApiController
    {
        //public HttpResponseMessage Get(string )
        //{
        //    var context = new WebAPIDemoContext();

        //    var product = context
        //        .Products
        //        .FirstOrDefault(e => e.ProductNumber == productNo);

        //    if (product == null) throw new Exception("Product not found!");

        //    return product;
        //}
        //[Authorize]
        public IHttpActionResult Get( int id)
        {

            var context = new AdirContext();
            var loginx = context.AppUsers.FirstOrDefault(p => p.AppUserId == id);
            return Ok(loginx);
        }

        //public IHttpActionResult Get(LoginDto login)
        //{

        //    var context = new AdirContext();
        //    var loginx = context.AppUsers.FirstOrDefault(p => p.Username == login.Username && p.PasswordHash == login.Password);
        //    return Ok(loginx);
        //}
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] AppUser user)
        {
            var context = new AdirContext();

            var passwordUpdate = context
                                .AppUsers
                                .FirstOrDefault(e => e.AppUserId == id);
           if (passwordUpdate == null) return new HttpResponseMessage(HttpStatusCode.NotFound);
           
            passwordUpdate.PasswordHash = user.PasswordHash;
            context.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

    }
}
