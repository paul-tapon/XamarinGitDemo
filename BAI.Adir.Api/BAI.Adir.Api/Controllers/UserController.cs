using BAI.Adir.Api.Domain.Context;
using BAI.Adir.Api.Domain.Model;
using BAI.Adir.Api.Helper;
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
        public IHttpActionResult Get(int id)
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
        public HttpResponseMessage Put(int id, [FromBody] ChangePasswordDto changePass)
        {
            if (id != changePass.AppUserId)
                return new HttpResponseMessage(HttpStatusCode.NotFound); //invalid submission

            var context = new AdirContext();
            var passwordUpdate = context
                                .AppUsers
                                .FirstOrDefault(e => e.AppUserId == id);

            if (passwordUpdate == null) return new HttpResponseMessage(HttpStatusCode.NotFound); //user not found

            var genSalt = EncryptionHelper.CreateSalt();

            if (!EncryptionHelper.CheckPassword(changePass.OldPassword,passwordUpdate.PasswordHash,passwordUpdate.PasswordSalt))
                return new HttpResponseMessage(HttpStatusCode.NotFound); //old password is wrong

            passwordUpdate.PasswordHash = EncryptionHelper.EncryptPassword(changePass.NewPassword, genSalt);
            passwordUpdate.PasswordSalt = genSalt;

            context.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

    }
}
