using System;
using System.Collections.Generic;

namespace StudentPortal.Models.Models
{
    public partial class ForgotPassword
    {
        public long LoginId { get; set; }
        public string UniqueKey { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Logins Login { get; set; }
    }
}
