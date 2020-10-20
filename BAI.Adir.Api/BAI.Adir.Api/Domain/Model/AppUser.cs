using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAI.Adir.Api.Domain.Model
{
    public class AppUser
    {
        [Key]
        public int AppUserId { get; set; }

        [Required]
        [MaxLength(50)]
        [Index("UsernameIndex", IsUnique = true)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        [Index("EmailIndex", IsUnique = true)]
        [RegularExpression("^([ña-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }


        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string MiddleName { get; set; }

        public DateTime? LastLoggedIn { get; set; }

        public int AccessFailedCount { get; set; }

        [MaxLength(500)]
        public string PasswordHash { get; set; }


        [NotMapped]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }
    }
}