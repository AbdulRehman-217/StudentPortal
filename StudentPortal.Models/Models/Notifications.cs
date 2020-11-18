using System;
using System.Collections.Generic;

namespace StudentPortal.Models.Models
{
    public partial class Notifications
    {
        public Notifications()
        {
            TimeTable = new HashSet<TimeTable>();
        }

        public long NotificationId { get; set; }
        public long? UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public bool IsRead { get; set; }
        public string TargetScreen { get; set; }
        public bool IsImportant { get; set; }
        public bool IsVisitorNotification { get; set; }
        public bool IsAllStudentNotification { get; set; }

        public virtual UserProfile User { get; set; }
        public virtual ICollection<TimeTable> TimeTable { get; set; }
    }
}
