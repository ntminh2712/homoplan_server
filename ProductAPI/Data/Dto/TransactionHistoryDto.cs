using System;

namespace SeminarAPI.Data.Dto
{
    public class TransactionHistoryDto
    {
        public string? transaction_history_id { get; set; }  
        public string? user_id { get; set; }
        public string? daily_tasks_id { get; set; }
        public string? challenge_tasks_id { get; set; }
        public string? reward_amount { get; set; }
        public string? type { get; set; }
        public int? status { get; set; }
        public DateTime? created_at { get; set; }
    }
}
