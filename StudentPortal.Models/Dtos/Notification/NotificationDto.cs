using System;
using System.Collections.Generic;
using System.Text;

namespace StudentPortal.Models.Dtos.Notification
{
    public class NotificationDto
    {
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public bool IsRead { get; set; }
        public string TargetScreen { get; set; }
        public bool IsImportant { get; set; }

    }
}
