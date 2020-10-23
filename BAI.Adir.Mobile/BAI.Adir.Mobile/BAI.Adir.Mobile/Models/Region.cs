using System;
using System.Collections.Generic;
using System.Text;

namespace BAI.Adir.Mobile.Models
{
   public class Region
    {
        public int RegionId { get; set; }

      
        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
