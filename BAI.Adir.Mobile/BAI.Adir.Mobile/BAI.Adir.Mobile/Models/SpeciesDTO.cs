using System;
using System.Collections.Generic;
using System.Text;

namespace BAI.Adir.Mobile.Models
{
    public class SpeciesDTO
    {
        public int SpeciesId { get; set; }
        public string Name { get; set; }
        public Boolean IsActive { get; set; }
        public int? CreatedByAppUserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedByAppUserId { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}
