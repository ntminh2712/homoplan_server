using Microsoft.AspNetCore.Http;
using System;

namespace SeminarAPI.Data.Dto
{
    public class RegisterDto
    {
        public string? User_Name { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Full_Name { get; set; }
        public string? Country { get; set; }
        public string? Email { get; set; }
        public IFormFile? avatar { get; set; }
        public string? partner_id { get; set; }
    }
}
