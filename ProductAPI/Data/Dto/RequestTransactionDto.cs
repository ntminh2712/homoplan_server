using System;

namespace SeminarAPI.Data.Dto
{
    public class RequestTransactionDto
    {
        public string? user_id { get; set; }
        public string? daily_tasks_id { get; set; }
        public string? challenge_tasks_id { get; set; }
        public string? type { get; set; }
        public int? status { get; set; }
    }
}
