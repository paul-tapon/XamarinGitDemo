using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAI.Adir.Api.Domain.Model
{
    public class DiseaseIncidentFile
    {
        [Key]
        public int DiseaseIncidentFileId { get; set; }

        [ForeignKey("DiseaseIncident")]
        public int DiseaseIncidentId { get; set; }

        public DiseaseIncident DiseaseIncident { get; set; }

        [ForeignKey("ContentFile")]
        public int ContentFileId { get; set; }

        public ContentFile ContentFile { get; set; }
    }
}