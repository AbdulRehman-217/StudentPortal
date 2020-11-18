using System;
using System.Collections.Generic;

namespace StudentPortal.Models.Models
{
    public partial class TimeTable
    {
        public long TimeTableId { get; set; }
        public long NotificationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FileUrl { get; set; }
        public bool? IsActive { get; set; }

        public virtual Notifications Notification { get; set; }
    }
}
