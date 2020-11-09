using System;
using System.Collections.Generic;
using System.Text;
using StudentPortal.Models.Dtos.User;

namespace StudentPortal.Models.Dtos.Login
{
    public class LoginResponseDto : UserProfileDto
    {
        public long UserId { get; set; }
        public int? RoleId { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public bool IsAccountVerified { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Token { get; set; }
    }
}
