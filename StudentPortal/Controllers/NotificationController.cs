using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models.Dtos.Notification;
using StudentPortal.Models.Models;
using StudentPortal.Utilities;

namespace StudentPortal.Controllers
{
    public class NotificationController : Controller
    {
        private readonly StudentPortalContext _context;

        public NotificationController(StudentPortalContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult VisitorNotification()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VisitorNotification(NotificationDto model)
        {
            NotificationsHandler.SendPushNotification("/topics/visitor",model.Message,model.Title,model.Type);
            var notification = new Notifications()
            {
                Title = model.Title,
                Type = model.Type,
                Message = model.Message,
                TargetScreen = model.Type,
                IsImportant = model.IsImportant
            };
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
            return Json(true);
        }
        public IActionResult AllStudentNotification()
        {
            return View();
        }
        public IActionResult SpecificStudentNotification()
        {
            return View();
        }
        public IActionResult BatchNotification()
        {
            return View();
        }
        public IActionResult SectionNotification()
        {
            return View();
        }
    }
}
