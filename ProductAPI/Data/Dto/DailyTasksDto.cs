using System;

namespace SeminarAPI.Data.Dto
{
    public class DailyTasksDto
    {
        public string? daily_tasks_id { get; set; }  
        public string? icon { get; set; }
        public string? title { get; set; }
        public string? Description { get; set; }
        public string? link { get; set; }
        public int? status { get; set; }
        public string? reward_amount { get; set; }
        public DateTime? created_at { get; set; }
    }
}
