using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StudentPortal.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult VisitorNotification()
        {
            return View();
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
