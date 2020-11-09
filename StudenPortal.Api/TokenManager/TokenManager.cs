using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace StudenPortal.Api.TokenManager
{
    public class TokenManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetEmail()
        {
            string tokenstring = "";

            var data = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (data.Count > 0 && !(data.First().Equals("Bearer")))
            {
                if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var traceValue))
                {
                    tokenstring = traceValue;
                }
                if (!string.IsNullOrEmpty(tokenstring))
                {
                    var jwtEncodedString = tokenstring.Substring(7); // trim 'Bearer ' from the start
                    var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
                    string connectionString = token.Claims.First(c => c.Type == "Email").Value;
                    if (!string.IsNullOrEmpty(connectionString)) return connectionString;
                }
            }

            return "";
        }

        public string GetUserName()
        {
            string tokenstring = "";

            var data = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (data.Count > 0 && !(data.First().Equals("Bearer")))
            {
                if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var traceValue))
                {
                    tokenstring = traceValue;
                }
                if (!string.IsNullOrEmpty(tokenstring))
                {
                    var jwtEncodedString = tokenstring.Substring(7); // trim 'Bearer ' from the start
                    var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
                    string userName = token.Claims.First(c => c.Type == "UserName").Value;
                    if (!string.IsNullOrEmpty(userName)) return userName;
                }
            }

            return "";
        }

        public long GetUserRoleId()
        {
            string tokenstring = "";

            var data = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (data.Count > 0 && !(data.First().Equals("Bearer")))
            {
                if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var traceValue))
                {
                    tokenstring = traceValue;
                }
                if (!string.IsNullOrEmpty(tokenstring))
                {
                    var jwtEncodedString = tokenstring.Substring(7); // trim 'Bearer ' from the start
                    var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
                    long RoleId = Convert.ToInt64(token.Claims.First(c => c.Type == "RoleId").Value);
                    return RoleId;
                }
            }

            return 0;
        }


        public long GetUserId()
        {
            string tokenstring = "";

            var data = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (data.Count > 0 && !(data.First().Equals("Bearer")))
            {
                if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var traceValue))
                {
                    tokenstring = traceValue;
                }
                if (!string.IsNullOrEmpty(tokenstring))
                {
                    var jwtEncodedString = tokenstring.Substring(7); // trim 'Bearer ' from the start
                    var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
                    long userId = Convert.ToInt32(token.Claims.First(c => c.Type == "UserId").Value);
                    return userId;
                }
            }

            return 0;
        }
    }
}
