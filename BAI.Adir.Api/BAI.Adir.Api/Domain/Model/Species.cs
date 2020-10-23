using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BAI.Adir.Api.Domain.Model
{
    public class Species : ModelBase
    {
        [Key]
        public int SpeciesId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string CommonDiseases { get; set; }
            }
}