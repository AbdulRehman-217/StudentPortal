using System;
using System.Collections.Generic;

namespace StudentPortal.Models.Models
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            Logins = new HashSet<Logins>();
            Notifications = new HashSet<Notifications>();
        }

        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ProfileUrl { get; set; }
        public string About { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Semester { get; set; }
        public string Section { get; set; }
        public string BatchStart { get; set; }
        public string BatchEnd { get; set; }
        public string Program { get; set; }

        public virtual DeviceTokens DeviceTokens { get; set; }
        public virtual ICollection<Logins> Logins { get; set; }
        public virtual ICollection<Notifications> Notifications { get; set; }
    }
}
