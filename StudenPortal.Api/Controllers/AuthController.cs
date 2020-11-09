using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using StudentPortal.Models.Dtos;
using StudentPortal.Models.Dtos.Login;
using StudentPortal.Models.Models;

namespace StudentPortal.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly StudentPortalContext _context;

        public AuthController(IMapper mapper, StudentPortalContext context, IConfiguration config)
        {
            _config = config;
            _mapper = mapper;
            _context = context;
        }

        [NonAction]
        private string GenerateJSONWebToken(LoginResponseDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                new List<Claim>()
                {
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("RoleId", user.RoleId.ToString()),
                    new Claim("UserName",user.FirstName ?? "" + "" + user.LastName ?? "")
                },
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto authUserDto)
        {
            var response = new ResponseDto<LoginResponseDto>();
            var result = await (from logins in _context.Logins
                join userRoles in _context.UserRoles on logins.RoleId equals userRoles.RoleId
                join profile in _context.UserProfile on logins.UserId equals profile.UserId
                where logins.Email.Equals(authUserDto.Email) && logins.Password == authUserDto.Password
                select new LoginResponseDto
                {
                    UserId = logins.UserId,
                    Email = logins.Email,
                    RoleId = logins.RoleId,
                    Role = userRoles.Title,
                    IsActive = logins.IsActive.Value,
                    IsAccountVerified = logins.IsAccountVerified,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Phone = profile.Phone,
                    Address1 = profile.Address1,
                    Address2 = profile.Address2,
                    ProfileUrl = profile.ProfileUrl,
                    CityId = profile.CityId,
                    CountryId = profile.CountryId,
                    StateId = profile.StateId,
                    ZipCode = profile.ZipCode,
                    About = profile.About,
                    CreatedDate = profile.CreatedDate
                }).FirstOrDefaultAsync();
            if (!(result is null))
            {
                response.Data = result;
                response.Data.Token = GenerateJSONWebToken(response.Data);
                return Ok(response);
            }
            response.Data = null;
            response.Status = 0;
            response.Message = "Invalid email or password";
            return Ok(response);
        }
    }
}