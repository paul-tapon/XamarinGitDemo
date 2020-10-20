using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAI.Adir.Api.Domain.Model
{
    public class Municipality : ModelBase
    {
        [Key]
        public int MunicipalityId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Abbreviation { get; set; }

        [MaxLength(6)]
        public string ZipCode { get; set; }

        [ForeignKey("Province")]
        public int? ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}