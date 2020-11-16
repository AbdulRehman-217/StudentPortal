using System;
using System.Collections.Generic;

namespace StudentPortal.Models.Models
{
    public partial class DeviceTokens
    {
        public long UserId { get; set; }
        public string DeviceToken { get; set; }

        public virtual UserProfile User { get; set; }
    }
}
