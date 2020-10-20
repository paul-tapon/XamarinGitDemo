using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAI.Adir.Api.Domain.Model
{
    public class DiseaseIncident : ModelBase
    {
        [Key]
        public int DiseaseIncidentId { get; set; }

        public string ReportedByFirstname { get; set; }

        public string ReportedByLastname { get; set; }

        public string ReportedByMiddleName { get; set; }

        public string ReportedBySuffixName { get; set; }

        public string ContactNumber { get; set; }

        public string ContactPerson { get; set; }


        /// <summary>
        /// Kelan nag-simula yung symptoms ng sakit
        /// </summary>
        public DateTime? SymptomsStartDate { get; set; }

        /// <summary>
        /// Kelan namatay
        /// </summary>
        public DateTime? DeceasedDate { get; set; }

        public int NumberOfInfected { get; set; }

        public int NumberOfDeaths { get; set; }

        public int Population { get; set; }

        public string Symptoms { get; set; }

        public string Notes { get; set; }

        //location
        [ForeignKey("Barangay")]
        public int BarangayId { get; set; }
        public Barangay Barangay { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}