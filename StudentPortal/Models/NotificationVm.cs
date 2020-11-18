using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using StudentPortal.Models.Dtos.Notification;

namespace StudentPortal.Models
{
    public class NotificationVm
    {
        public NotificationVm()
        {
            NotificationDto = new NotificationDto();
        }
        public IFormFile TimeTableFile { get; set; }
        public NotificationDto NotificationDto { get; set; }


    }
}
