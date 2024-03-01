using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SeminarAPI.Data.Model
{
    public class Users : BaseEntity
    {
        [Key]
        public string User_Id { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Full_Name { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string avatar { get; set; }
        public string Reference_Id { get; set; }
        public int reference_count { get; set; }
        public string partner_id { get; set; }

    }
}
