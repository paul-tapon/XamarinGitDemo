using System;
using System.Collections.Generic;
using System.Text;

namespace BAI.Adir.Mobile.Models
{
    public class Province
    {
        public int ProvinceId { get; set; }

       public string Name { get; set; }

        public string Abbreviation { get; set; }

    
        public int? RegionId { get; set; }
        public virtual Region Region { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
