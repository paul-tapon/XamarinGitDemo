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

        public int RegionId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public Boolean IsActive { get; set; }
        public int? CreatedByAppUserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedByAppUserId { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}
