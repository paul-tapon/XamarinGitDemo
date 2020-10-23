using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;
using BAI.Adir.Api.Domain.Context;
using BAI.Adir.Api.Domain.Model;

namespace BAI.Adir.Api.Controllers
{
    public class DiseaseIncidentsController : ApiController
    {
        private AdirContext db = new AdirContext();

        // GET: api/DiseaseIncidents
        public IQueryable<DiseaseIncident> GetDiseaseIncidents()
        {
            return db.DiseaseIncidents;
        }

        // GET: api/DiseaseIncidents/5
        [ResponseType(typeof(DiseaseIncident))]
        public IHttpActionResult GetDiseaseIncident(int id)
        {
            DiseaseIncident diseaseIncident = db.DiseaseIncidents.Find(id);
            if (diseaseIncident == null)
            {
                return NotFound();
            }

            return Ok(diseaseIncident);
        }

        // PUT: api/DiseaseIncidents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDiseaseIncident(int id, DiseaseIncident diseaseIncident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != diseaseIncident.DiseaseIncidentId)
            {
                return BadRequest();
            }

            db.Entry(diseaseIncident).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiseaseIncidentExists(id))
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

        ////[HttpGet]
        ////[Route("api/diseaseincident/displayPhoto/{id}/{randomNumber}")]
        //public HttpResponseMessage GetDisplayPhoto(int id, string randomNumber)
        //{
        //    var context = new AdirContext();

        //    var DiseaseUpdate = context
        //                   .DiseaseIncidents
        //                   .FirstOrDefault(e => e.DiseaseIncidentId == id);
        //    string fileDirectory = @"c:\xamarin-uploads";

        //    var filename = DiseaseUpdate.DisplayPhotoFileName ?? "Default.jpg";

        //    var fullFilePath = Path.Combine(fileDirectory, filename);
        //    string mimeType = string.Empty;

        //    switch (Path.GetExtension(filename.ToLower()))
        //    {
        //        case ".jpg":
        //            mimeType = "image/jpg";
        //            break;
        //        case ".png":
        //            mimeType = "image/png";
        //            break;
        //        default:
        //            mimeType = "image/bmp";
        //            break;
        //    }

        //    HttpResponseMessage response = new HttpResponseMessage();
        //    response.Content = new StreamContent(new FileStream(fullFilePath, FileMode.Open, FileAccess.Read)); // this file stream
        //    response.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
        //    response.Headers.Add("Cache-Control", "no-cache");
        //    return response;


            // POST: api/DiseaseIncidents
            [ResponseType(typeof(DiseaseIncident))]
        public IHttpActionResult PostDiseaseIncident(DiseaseIncident diseaseIncident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DiseaseIncidents.Add(diseaseIncident);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = diseaseIncident.DiseaseIncidentId }, diseaseIncident);
        }

        // DELETE: api/DiseaseIncidents/5
        [ResponseType(typeof(DiseaseIncident))]
        public IHttpActionResult DeleteDiseaseIncident(int id)
        {
            DiseaseIncident diseaseIncident = db.DiseaseIncidents.Find(id);
            if (diseaseIncident == null)
            {
                return NotFound();
            }

            db.DiseaseIncidents.Remove(diseaseIncident);
            db.SaveChanges();

            return Ok(diseaseIncident);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiseaseIncidentExists(int id)
        {
            return db.DiseaseIncidents.Count(e => e.DiseaseIncidentId == id) > 0;
        }
    }
}