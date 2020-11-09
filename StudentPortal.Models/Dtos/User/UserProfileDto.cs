using System.ComponentModel.DataAnnotations;
using StudentPortal.Models.Dtos.Login;

namespace StudentPortal.Models.Dtos.User
{
    public class UserProfileDto : LoginDto
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
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string ZipCode { get; set; }
        public string About { get; set; }

    }
}
