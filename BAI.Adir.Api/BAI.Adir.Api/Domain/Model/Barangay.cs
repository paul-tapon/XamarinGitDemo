using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAI.Adir.Api.Domain.Model
{
    public class Barangay : ModelBase
    {
        [Key]
        public int BarangayId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [ForeignKey("Municipality")]
        public int MunicipalityId { get; set; }
        public virtual Municipality Municipality { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}