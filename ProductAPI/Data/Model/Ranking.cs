using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarAPI.Data.Model
{
    [Table("ranking")]
    public class Ranking : BaseEntity
    {
        [Key]
        public string? ranking_id { get; set; }
        public string? user_id { get; set; }
        public string? reward_amount { get; set; }
        public string? type { get; set; }
        public int? status { get; set; }
    }
}
