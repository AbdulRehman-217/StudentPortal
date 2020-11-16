using System;
using System.ComponentModel.DataAnnotations;
using StudentPortal.Models.Dtos.DeviceToken;
using StudentPortal.Models.Dtos.Login;

namespace StudentPortal.Models.Dtos.User
{
    public class UserProfileDto : LoginDto
    {
       
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ProfileUrl { get; set; }
        public string About { get; set; }
        public string Semester { get; set; }
        public string Section { get; set; }
        public string BatchStart { get; set; }
        public string BatchEnd { get; set; }
        public string Program { get; set; }

        [Required]
        public  string DeviceToken { get; set; }

    }
}
