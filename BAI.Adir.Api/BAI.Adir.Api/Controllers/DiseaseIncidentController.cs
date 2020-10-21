using BAI.Adir.Api.Domain.Context;
using BAI.Adir.Api.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;



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

        public IHttpActionResult Get(string region,string province, string municipality, string barangay, string species, string sortField, string sortDir)
        {
            var context = new AdirContext();

            IQueryable<DiseaseIncident>
                query = context.DiseaseIncidents;

            if (!string.IsNullOrEmpty(region))
            {
                query = query.Where(p => p.Barangay.Municipality.Province.Region.Name == region);
                    
            }
            return Ok(query.ToList());
        }
    }
}
