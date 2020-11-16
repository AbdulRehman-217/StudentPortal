using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudenPortal.Api.TokenManager;
using StudenPortal.Utilities;
using StudentPortal.Models.Dtos;
using StudentPortal.Models.Dtos.User;
using StudentPortal.Models.Models;

namespace StudentPortal.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly StudentPortalContext _context;
        private readonly TokenManager _tokenManager;

        public UsersController(IMapper mapper, StudentPortalContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _tokenManager = new TokenManager(httpContextAccessor);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserProfileDto userDto)
        {
            var response = new ResponseDto<bool>();
            var isEmailExist = await _context.Logins.AnyAsync(x => x.Email == userDto.Email);
            if (isEmailExist)
            {
                response.Data = false;
                response.Message = "Email Already Exist";
                response.Status = 0;
                return Ok(response);
            }
            var mapped = _mapper.Map<UserProfile>(userDto);
            var loginDetail = new Logins
            {
                UserName = userDto.FirstName + " " + userDto.LastName,
                Password = userDto.Password,
                Email = userDto.Email,
                RoleId = 2
            };
           
            mapped.DeviceTokens = new DeviceTokens();
            mapped.DeviceTokens.DeviceToken = userDto.DeviceToken;
            mapped.CreatedDate = DateTime.Now;
            mapped.Logins.Add(loginDetail);
            await _context.UserProfile.AddAsync(mapped);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                response.Data = true;
                return Ok(response);
            }
            response.Data = false;
            response.Message = "Something Went Wrong";
            response.Status = 0;
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfile(UpdateProfileDto model)
        {
            var response = new ResponseDto<bool>();
            var userId = _tokenManager.GetUserId();
            var userFromRepo = await _context.UserProfile.FirstOrDefaultAsync(x => x.UserId == userId);
            if (userFromRepo == null)
            {
                response.Data = false;
                response.Message = "Email Already Exist";
                response.Status = 0;
                return Ok(response);
            }
            var mapped = _mapper.Map<UpdateProfileDto, UserProfile>(model, userFromRepo);
            mapped.ModifiedDate = DateTime.Now;
            _context.Entry(mapped).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            response.Data = true;
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var response = new ResponseDto<bool>();
            var userFromRepo = await _context.Logins.Include(f => f.ForgotPassword).FirstOrDefaultAsync(x => x.Email == email && x.IsActive == true);
            if (userFromRepo is null)
            {
                response.Data = false;
                response.Message = "User Not Exist";
                response.Status = 0;
                return Ok(response);
            }
            if (userFromRepo.IsActive != null && (userFromRepo.IsAccountVerified && userFromRepo.IsActive.Value))
            {
                var uniqueKey = CommonMethods.GetUniqueIdentifier();
                if (userFromRepo.ForgotPassword is null)
                {
                    var forgotPassword = new ForgotPassword()
                    {
                        LoginId = userFromRepo.LoginId,
                        UniqueKey = uniqueKey,
                        CreatedDate = DateTime.Now
                    };
                    await _context.ForgotPassword.AddAsync(forgotPassword);
                }
                else
                {
                    userFromRepo.ForgotPassword.UniqueKey = uniqueKey;
                }
                await _context.SaveChangesAsync();
                //send email
                // EmailSender.SendForgotPasswordEmail(userFromRepo.Email, userFromRepo.UserName, uniqueKey);
                response.Data = true;
                response.Message = "Email Sent";
                return Ok(response);
            }
            response.Data = false;
            response.Message = "User Not Verified";
            response.Status = 0;
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            var response = new ResponseDto<bool>();
            var userId = _tokenManager.GetUserId();
            var userFromRepo = await _context.Logins.FirstOrDefaultAsync(x => x.UserId == userId && x.Password == model.OldPassword);
            if (userFromRepo is null)
            {
                response.Data = false;
                response.Message = "Wrong Password";
                response.Status = 0;
                return Ok(response);
            }
            if (userFromRepo.IsActive != null && userFromRepo.IsActive.Value)
            {
                userFromRepo.Password = model.Password;
                await _context.SaveChangesAsync();
                response.Data = true;
                return Ok(response);
            }
            response.Data = false;
            response.Message = "User Blocked";
            response.Status = 0;
            return Ok(response);

        }

        [HttpGet("{email}")]
        public async Task<IActionResult> IsEmailAvailable(string email)
        {
            var response = new ResponseDto<bool>();
            var result = await _context.Logins.AnyAsync(x => x.Email == email);
            if (result)
            {
                response.Message = "Email Already Exist";
                response.Data = false;
                response.Status = 0;
            }
            else
            {
                response.Data = true;
                response.Message = "Available";
            }
            return Ok(response);
        }
    }
}