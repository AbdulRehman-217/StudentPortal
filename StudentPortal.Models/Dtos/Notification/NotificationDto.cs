using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentPortal.Models.Dtos.Notification
{
    public class NotificationDto
    {
        public long UserId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]

        public string Message { get; set; }

        [Required]
        public string Type { get; set; }
        public bool IsRead { get; set; }
        public string TargetScreen { get; set; }
        public bool IsImportant { get; set; } = false;


    }
}
