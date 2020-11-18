using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models;
using StudentPortal.Models.Dtos.Notification;
using StudentPortal.Models.Models;
using StudentPortal.Utilities;
using Trough.Utilities;

namespace StudentPortal.Controllers
{
    public class NotificationController : Controller
    {
        private readonly stdportalContext _context;

        public NotificationController(stdportalContext context)
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
            NotificationsHandler.SendPushNotification("/topics/visitor", model.Message, model.Title, model.Type);
            var notification = new Notifications()
            {
                Title = model.Title,
                Type = model.Type,
                Message = model.Message,
                TargetScreen = model.Type,
                IsImportant = model.IsImportant,
                IsVisitorNotification = true
            };
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> StudentNotification(NotificationVm model)
        {
            var url = "";
            //NotificationsHandler.SendPushNotification("/topics/student", model.Message, model.Title, model.Type);
            if (!(model.TimeTableFile is null))
            {
                url = await CommonMethods.UploadFile(model.TimeTableFile);
            }
            var notification = new Notifications()
            {
                Title = model.NotificationDto.Title,
                Type = model.NotificationDto.Type,
                Message = model.NotificationDto.Message,
                TargetScreen = model.NotificationDto.Type,
                IsAllStudentNotification = true
            };
            if (!(string.IsNullOrEmpty(url)))
            {
                notification.TimeTable.Add(new TimeTable()
                {
                    CreatedDate = DateTime.Now,
                    FileUrl = url,
                    IsActive = true
                });
            }

            try
            {
                await _context.Notifications.AddAsync(notification);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
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

        [HttpGet]
        public IActionResult Uploads(string id)
        {
            var fileDetail = "";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot") + "/Uploads/"+id;
            if (fileDetail != null)
            {
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = "Test.pdf",
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());

                //get physical path
                var fileReadPath = filePath;

                var file = System.IO.File.OpenRead(fileReadPath);
                return File(file, "APPLICATION/octet-stream","test1.pdf");
            }
            //else
            //{
            //    return StatusCode(404);
            //}

            return View("AllStudentNotification");
        }
    }
}

