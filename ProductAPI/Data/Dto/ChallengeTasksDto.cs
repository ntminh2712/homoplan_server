using System;

namespace SeminarAPI.Data.Dto
{
    public class ChallengeTasksDto
    {
        public string? challenge_tasks_id { get; set; }  
        public string? icon { get; set; }
        public string? title { get; set; }
        public string? Description { get; set; }
        public string? link { get; set; }
        public int? status { get; set; }
        public int? level { get; set; }
        public string? reward_amount { get; set; }
        public DateTime? created_at { get; set; }
    }
}
