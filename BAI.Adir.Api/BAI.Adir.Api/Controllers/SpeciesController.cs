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
using System.Web.UI.WebControls.Expressions;
using BAI.Adir.Api.Domain.Context;
using BAI.Adir.Api.Domain.Model;

namespace BAI.Adir.Api.Controllers
{
    public class SpeciesController : ApiController
    {
        private AdirContext db = new AdirContext();

        // GET: api/Species
        public List<Species> GetSpecies()
        {
            return db.Species.OrderBy(p=>p.Name).ToList();
        }

        // GET: api/Species/5
        [ResponseType(typeof(Species))]
        public IHttpActionResult GetSpecies(int id)
        {
            Species species = db.Species.Find(id);
            if (species == null)
            {
                return NotFound();
            }

            return Ok(species);
        }

        // PUT: api/Species/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSpecies(int id, [FromBody]Species species)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != species.SpeciesId)
            {
                return BadRequest();
            }
            
            species.ModifiedOn = DateTime.Now;
            db.Entry(species).State = EntityState.Modified;

            try
            {

               
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpeciesExists(id))
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

        // POST: api/Species
        [ResponseType(typeof(Species))]
        public IHttpActionResult PostSpecies(Species species)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Species.Add(species);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = species.SpeciesId }, species);
        }

        // DELETE: api/Species/5
        [ResponseType(typeof(Species))]
        public IHttpActionResult DeleteSpecies(int id)
        {
            Species species = db.Species.Find(id);
            if (species == null)
            {
                return NotFound();
            }

            db.Species.Remove(species);
            db.SaveChanges();

            return Ok(species);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SpeciesExists(int id)
        {
            return db.Species.Count(e => e.SpeciesId == id) > 0;
        }

        //FILTER
        [Route("api/Species/Filter")]
        public IHttpActionResult Get(string value)

        {
            var context = new AdirContext();

            IQueryable<Species> query = context.Species;
        
                if (!string.IsNullOrEmpty(value))
                {
                    query = query.Where(p => p.Name.Contains(value));
                }
                query = query.OrderBy(p => p.Name);
                var responseData = query.ToList();
                 


            return Ok(responseData);

           
          
        }
    }
}