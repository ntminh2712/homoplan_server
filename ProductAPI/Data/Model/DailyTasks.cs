using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarAPI.Data.Model
{
    [Table("daily_tasks")]
    public class DailyTasks : BaseEntity
    {
        [Key]
        public string? daily_tasks_id { get; set; }
        public string? icon { get; set; }
        public string? title { get; set; }
        public string? Description { get; set; }
        public string? link { get; set; }
        public int? status { get; set; } // 0 là chưa hoàn thành, 1 là đã hoàn thành
        public string? reward_amount { get; set; }
    }
}
