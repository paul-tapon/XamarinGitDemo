using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAI.Adir.Mobile.Models
{
    public class DiseaseIncident
    {
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

        public int BarangayId { get; set; }
        public Barangay Barangay { get; set; }

        

        public double Latitude { get; set; }
        public double Longitude
        {
            get; set;
        }

        public int SpeciesId { get; set; }

        public Species Species { get; set; }
    

        public string FullAddress
        {
            get
            {

                //return "Brgy : "+  ;Barangay != null ? Barangay.Name :"";
                return Barangay.Name + " " + Barangay.Municipality.Name + " " + Barangay.Municipality.Province.Name;

            }
        }

        public bool IsActive { get; internal set; }
        public int CreatedByAppUserId { get; internal set; }
        public DateTime CreatedOn { get; internal set; }
    }
}
