using System;
using System.Collections.Generic;

namespace StudentPortal.Models.Models
{
    public partial class UserRoles
    {
        public UserRoles()
        {
            Logins = new HashSet<Logins>();
        }

        public int RoleId { get; set; }
        public string Title { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedById { get; set; }

        public virtual ICollection<Logins> Logins { get; set; }
    }
}
