using BAI.Adir.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BAI.Adir.Mobile.Services
{
    internal class APIServices
    {

        public async Task<List<SpeciesDTO>> GetSpecies()
        {
            var client = new HttpClient();
            var json = await client.GetStringAsync(Settings.AdirApiUrl + "api/species");
            var species = JsonConvert.DeserializeObject<List<SpeciesDTO>>(json);
            return species;
        }
    }
}
