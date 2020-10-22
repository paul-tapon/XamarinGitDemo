using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using BAI.Adir.Api.Domain.Context;
using BAI.Adir.Api.Domain.Model;
using BAI.Adir.Api.Helper;
using BAI.Adir.Api.Models.DTO;

namespace BAI.Adir.Api.Controllers
{
    public class AppUsersController : ApiController
    {
        private AdirContext db = new AdirContext();

        // GET: api/AppUsers
        public IQueryable<AppUser> GetAppUsers()
        {
            return db.AppUsers;
        }
        // GET: api/AppUsers/5
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult GetAppUser(int id)
        {
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return NotFound();
            }

            return Ok(appUser);
        }

        [Route("api/appUsers/verify/")]
        public HttpResponseMessage GetVerify(int id)
        {
            var userToUpdate = db.AppUsers.FirstOrDefault(a => a.AppUserId == id);

            HttpResponseMessage hpo = new HttpResponseMessage(HttpStatusCode.Moved);
            hpo.Headers.Location = new Uri("https://localhost:44395?ok");
            HttpResponseMessage hp = new HttpResponseMessage(HttpStatusCode.Moved);
            hp.Headers.Location = new Uri("https://localhost:44395?notok");


            if (userToUpdate == null)
                return hp;

            userToUpdate.EmailConfirmed = true;
            db.SaveChanges();

            return hpo;


        }

        // PUT: api/AppUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppUser(int id, AppUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appUser.AppUserId)
            {
                return BadRequest();
            }

            db.Entry(appUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AppUsers
        //[Route("api/AppUsers")]
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult PostAppUser([FromBody] AppUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var genSalt = EncryptionHelper.CreateSalt();

            appUser.PasswordHash = EncryptionHelper.EncryptPassword(appUser.PasswordHash, genSalt);
            appUser.PasswordSalt = genSalt;
            
            db.AppUsers.Add(appUser);
           
            db.SaveChanges();

            if (SendVerificationMail(appUser.Email,appUser.AppUserId))
            {

                return CreatedAtRoute("DefaultApi", new { id = appUser.AppUserId }, appUser);

            }
            else
            {
                return BadRequest();
            }
        }
        [Route("api/RegisterAppUser")]
        [ResponseType(typeof(RegisterDto))]
        public IHttpActionResult PostRegisterAppUser([FromBody] RegisterDto registerDto)
        {
            var context = new AppUser();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var genSalt = EncryptionHelper.CreateSalt();

            context.FirstName = registerDto.FirstName;
            context.MiddleName = registerDto.MiddleName;
            context.LastName = registerDto.LastName;
            context.PasswordHash = EncryptionHelper.EncryptPassword(registerDto.PlainPassword, genSalt);
            context.PasswordSalt = genSalt;
            context.Email = registerDto.Email;
            context.Username = registerDto.Username;

            //appUser.PasswordHash = EncryptionHelper.EncryptPassword(appUser.PasswordHash, genSalt);

            db.AppUsers.Add(context);

            db.SaveChanges();

            if (SendVerificationMail(context.Email, context.AppUserId))
            {

                return CreatedAtRoute("DefaultApi", new { id = context.AppUserId }, context);

            }
            else
            {
                return BadRequest();
            }
        }

        public static bool SendVerificationMail(string emailaddress,int id)
        {
            try
            { 

                var fromaddress = new MailAddress("adir@firemail.cc", "[no-reply] ADIR");
                var toaddress = new MailAddress(emailaddress);
                string fromPassword = "147258369";
                string subject = "[NO-REPLY]";
                string body = "Your account has been registered. Please click <a href='https://localhost:44395/api/appUsers/verify?id="+id.ToString()+"'>here</a> to activate your account";
                SmtpClient smtp = new SmtpClient();
                MailMessage e_mail = new MailMessage();
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(fromaddress.Address, fromPassword);
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Host = "mail.cock.li";
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                e_mail = new MailMessage();
                e_mail.From = new MailAddress(fromaddress.Address, "[no-reply] ADIR");
                e_mail.To.Add(toaddress.Address);
                e_mail.Subject = subject;
                e_mail.IsBodyHtml = true;
                e_mail.Body = body;

                smtp.Send(e_mail);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        // DELETE: api/AppUsers/5
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult DeleteAppUser(int id)
        {
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return NotFound();
            }

            db.AppUsers.Remove(appUser);
            db.SaveChanges();

            return Ok(appUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppUserExists(int id)
        {
            return db.AppUsers.Count(e => e.AppUserId == id) > 0;
        }
    }
}