using BAI.Adir.Api.Domain.Context;
using BAI.Adir.Api.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace BAI.Adir.Api.Controllers
{
    public class DiseaseIncidentController : ApiController
    {
        public List<DiseaseIncident> Get()
        {
            var context = new AdirContext();
            return context.DiseaseIncidents.ToList();
        }

        [Route("api/DiseaseIncident/filter")]

        public IHttpActionResult Get(int region,int province, int municipality, int barangay, int species, string sortField, string sortDir)
        {
            var context = new AdirContext();

            IQueryable<DiseaseIncident>
                query = context.DiseaseIncidents;
            try
            {


                if (region != 0)
                {
                    query = query.Where(p => p.Barangay.Municipality.Province.Region.RegionId == region);

                }
                if (province !=0 )
                {
                    query = query.Where(p => p.Barangay.Municipality.Province.ProvinceId == province);

                }
                if (municipality != 0)
                {
                    query = query.Where(p => p.Barangay.Municipality.MunicipalityId == municipality);

                }
                if (species !=0 )
                {
                    query = query.Where(p => p.SpeciesId == species);
                }
                    

                return Ok(query.ToList());
            }
            catch
            {
                var data = query.ToList();
                return Ok(data);
            }
        }

        [ResponseType(typeof(DiseaseIncident))]
        public IHttpActionResult PostDiseaseIncident(DiseaseIncident diseaseIncident)
        {
            var context = new AdirContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.DiseaseIncidents.Add(diseaseIncident);
            context.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = diseaseIncident.DiseaseIncidentId }, diseaseIncident);
        }

    }


}
