using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarAPI.Data.Model
{
    [Table("challenge_tasks")]
    public class ChallengeTasks : BaseEntity
    {
        [Key]
        public string? challenge_tasks_id { get; set; }
        public string? icon { get; set; }
        public string? title { get; set; }
        public string? Description { get; set; }
        public string? link { get; set; }
        public int? status { get; set; } // 0 là chưa hoàn thành, 1 là đã hoàn thành
        public int? level { get; set; }
        public string? reward_amount { get; set; }
    }
}
