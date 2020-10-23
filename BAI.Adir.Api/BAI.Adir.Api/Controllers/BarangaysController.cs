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
    [RoutePrefix("Barangays")]
    public class BarangaysController : ApiController
    {
        private AdirContext db = new AdirContext();

        // GET: api/Barangays
        public IQueryable<Barangay> GetBarangays()
        {
            return db.Barangays;
        }

        [Route("GetBarangaysByMunicipalityId")]
        [HttpGet]
        public IHttpActionResult GetBarangaysByMunicipalityId(int id)
        {
            return Ok(db.Barangays.Where(p => p.MunicipalityId == id));
        }

        // GET: api/Barangays/5
        [ResponseType(typeof(Barangay))]
        public IHttpActionResult GetBarangay(int id)
        {
            Barangay barangay = db.Barangays.Find(id);
            if (barangay == null)
            {
                return NotFound();
            }

            return Ok(barangay);
        }

        // PUT: api/Barangays/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBarangay(int id, Barangay barangay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != barangay.BarangayId)
            {
                return BadRequest();
            }

            db.Entry(barangay).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarangayExists(id))
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

        // POST: api/Barangays
        [ResponseType(typeof(Barangay))]
        public IHttpActionResult PostBarangay(Barangay barangay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Barangays.Add(barangay);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = barangay.BarangayId }, barangay);
        }

        // DELETE: api/Barangays/5
        [ResponseType(typeof(Barangay))]
        public IHttpActionResult DeleteBarangay(int id)
        {
            Barangay barangay = db.Barangays.Find(id);
            if (barangay == null)
            {
                return NotFound();
            }

            db.Barangays.Remove(barangay);
            db.SaveChanges();

            return Ok(barangay);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BarangayExists(int id)
        {
            return db.Barangays.Count(e => e.BarangayId == id) > 0;
        }
    }
}