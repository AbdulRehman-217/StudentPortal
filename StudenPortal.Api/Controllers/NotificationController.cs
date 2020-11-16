using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Models.Dtos;
using StudentPortal.Models.Dtos.Notification;
using StudentPortal.Models.Models;

namespace StudenPortal.Api.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly StudentPortalContext _context;
        private readonly TokenManager.TokenManager _tokenManager;

        public NotificationController(IMapper mapper, StudentPortalContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _tokenManager = new TokenManager.TokenManager(httpContextAccessor);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetNotification()
        {
            var response = new ResponseDto<List<NotificationDto>>();
            var userId = _tokenManager.GetUserId();
            var notifications = await _context.Notifications.Where(x=>x.UserId == userId).ToListAsync();
            if (notifications.Count > 0)
            {
                response.Data = _mapper.Map<List<NotificationDto>>(notifications);
                return Ok(response);
            }
            response.Data = new List<NotificationDto>();
            response.Message = "No Notification Found";
            response.Status = 0;
            return Ok(response);

        }

    }
}