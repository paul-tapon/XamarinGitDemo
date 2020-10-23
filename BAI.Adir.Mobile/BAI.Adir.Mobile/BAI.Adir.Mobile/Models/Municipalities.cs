using System;
using System.Collections.Generic;
using System.Text;

namespace BAI.Adir.Mobile.Models
{
    public class Municipalities
    {
        public int MunicipalityId { get; set; }
        public string Name { get; set; }

        public string Abbreviation { get; set; }
        public string ZipCode { get; set; }

        public int ProvinceId { get; set; }

        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public Boolean IsActive { get; set; }
        public int? CreatedByAppUserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedByAppUserId { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
