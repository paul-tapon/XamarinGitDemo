using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BAI.Adir.Api.Domain.Context;
using BAI.Adir.Api.Domain.Model;

namespace BAI.Adir.Api.Controllers
{
    [RoutePrefix("api/Municipalities")]
    public class MunicipalitiesController : ApiController
    {
        private AdirContext db = new AdirContext();

        // GET: api/Municipalities
        public List<Municipality> GetMunicipalities()
        {
            return db.Municipalities.ToList();
        }


        [Route("GetMunicipalitiesByProvinceId")]
       // [HttpGet]
        public IHttpActionResult GetMunicipalitiesByProvinceId(int id)
        {
            return Ok(db.Municipalities.Where(p => p.ProvinceId == id).ToList());
        }

        // GET: api/Municipalities/5
        [ResponseType(typeof(Municipality))]
        public IHttpActionResult GetMunicipality(int id)
        {
            Municipality municipality = db.Municipalities.Find(id);
            if (municipality == null)
            {
                return NotFound();
            }

            return Ok(municipality);
        }

        // PUT: api/Municipalities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMunicipality(int id, Municipality municipality)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != municipality.MunicipalityId)
            {
                return BadRequest();
            }

            db.Entry(municipality).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MunicipalityExists(id))
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

        // POST: api/Municipalities
        [ResponseType(typeof(Municipality))]
        public IHttpActionResult PostMunicipality(Municipality municipality)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Municipalities.Add(municipality);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = municipality.MunicipalityId }, municipality);
        }

        // DELETE: api/Municipalities/5
        [ResponseType(typeof(Municipality))]
        public IHttpActionResult DeleteMunicipality(int id)
        {
            Municipality municipality = db.Municipalities.Find(id);
            if (municipality == null)
            {
                return NotFound();
            }

            db.Municipalities.Remove(municipality);
            db.SaveChanges();

            return Ok(municipality);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MunicipalityExists(int id)
        {
            return db.Municipalities.Count(e => e.MunicipalityId == id) > 0;
        }
    }
}