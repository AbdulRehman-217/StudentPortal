using System;
using System.Collections.Generic;

namespace StudentPortal.Models.Models
{
    public partial class Logins
    {
        public long LoginId { get; set; }
        public long UserId { get; set; }
        public string RollNo { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public long? CreatedById { get; set; }
        public long? ModifiedById { get; set; }
        public bool? IsActive { get; set; }
        public bool IsAccountVerified { get; set; }

        public virtual UserRoles Role { get; set; }
        public virtual UserProfile User { get; set; }
        public virtual ForgotPassword ForgotPassword { get; set; }
    }
}
