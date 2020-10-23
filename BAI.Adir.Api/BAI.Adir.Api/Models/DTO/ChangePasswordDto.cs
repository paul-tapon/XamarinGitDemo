using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAI.Adir.Api.Models.DTO
{
    public class ChangePasswordDto
    {
        public int AppUserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}