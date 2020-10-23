using System;
using System.Collections.Generic;
using System.Text;

namespace BAI.Adir.Mobile.Models
{
    public class Species
    {
        public int SpeciesId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string CommonDiseases { get; set; }

        public bool IsActive { get; set; }

        public int? CreatedByAppUserId { get; set; }
        public virtual AppUser CreatedByAppUser { get; set; }

        public DateTime CreatedOn { get; set; }


        public int? ModifiedByAppUserId { get; set; }
        public virtual AppUser ModifiedByAppUser { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
