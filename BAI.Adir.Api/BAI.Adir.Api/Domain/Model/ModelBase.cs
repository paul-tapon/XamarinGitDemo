using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace BAI.Adir.Api.Domain.Model
{
    public class ModelBase
    {
        public bool IsActive { get; set; }

        [ForeignKey("CreatedByAppUser")]
        public int? CreatedByAppUserId { get; set; }
        [ScriptIgnore]
        public virtual AppUser CreatedByAppUser { get; set; }

        public DateTime CreatedOn { get; set; }

        [ForeignKey("ModifiedByAppUser")]
        public int? ModifiedByAppUserId { get; set; }
        [ScriptIgnore]
        public virtual AppUser ModifiedByAppUser { get; set; }

        public DateTime? ModifiedOn { get; set; }

    }

}