using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentPortal.Models.Dtos.User
{
    public class UpdateProfileDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(25)]
        public string Phone { get; set; }

        [Required]
        [StringLength(150)]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ProfileUrl { get; set; }
        public string About { get; set; }
    }
}
