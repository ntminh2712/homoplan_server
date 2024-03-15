using Microsoft.AspNetCore.Http;
using System;

namespace SeminarAPI.Data.Dto
{
    public class UserLoginDto
    {
        public string? User_Name { get; set; }
        public string? Password { get; set; }
    }
}
