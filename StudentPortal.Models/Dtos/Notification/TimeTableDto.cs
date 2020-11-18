using System;
using System.Collections.Generic;
using System.Text;

namespace StudentPortal.Models.Dtos.Notification
{
    public class TimeTableDto
    {
        public DateTime CreatedDate { get; set; }
        public string FileUrl { get; set; }

        public virtual NotificationDto Notification { get; set; }

    }
}
