using System;
using System.Collections.Generic;

namespace StudentPortal.Models.Models
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            Logins = new HashSet<Logins>();
        }

        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ProfileUrl { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string ZipCode { get; set; }
        public string About { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<Logins> Logins { get; set; }
    }
}
