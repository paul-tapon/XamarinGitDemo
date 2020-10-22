using System;
using System.Collections.Generic;
using System.Text;

namespace BAI.Adir.Mobile.Models
{
    public class Municipality
    {
        public int MunicipalityId { get; set; }

        public string Name { get; set; }

      
        public string Abbreviation { get; set; }

      
        public string ZipCode { get; set; }

       
        public int? ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
