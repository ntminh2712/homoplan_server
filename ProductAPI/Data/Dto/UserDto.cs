using Microsoft.AspNetCore.Http;
using System;

namespace SeminarAPI.Data.Dto
{
    public class UserDto
    {
        public string? User_Id { get; set; }
        public string? User_Name { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Full_Name { get; set; }
        public string? Country { get; set; }
        public string? Email { get; set; }
        public string? Reference_Id { get; set; }
        public IFormFile? avatar { get; set; }
        public string? avatar_path { get; set; }
        public int? reference_count { get; set; }
        public string? partner_id { get; set; }
        public DateTime? Created_At { get; set; }
    }
}
