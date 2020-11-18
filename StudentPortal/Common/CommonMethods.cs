using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Trough.Utilities
{
    public static class CommonMethods
    {
        public static string GetUniqueIdentifier()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static async Task<string> UploadFile(IFormFile file)
        {
            var path = "";
            var directory = "";
            if (file != null)
            {
                var fileName = GetFileName(file.FileName);
                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads", fileName);
                directory = Path.Combine("/Uploads/", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return directory;
        }

        public static void DeleteFile(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            if (File.Exists(filePath+fileName))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception ex)
                {
                }
            }
        }
        public static string GetFileName(string fileName)
        {
            return string.Concat(
                Path.GetFileNameWithoutExtension(fileName),
                DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Path.GetExtension(fileName)
            );
        }

        public enum UserRoles
        {
            Administrator = 1,
            User = 2,
            Truck = 3
        }
    }
}
