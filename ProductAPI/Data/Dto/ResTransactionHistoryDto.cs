using System;

namespace SeminarAPI.Data.Dto
{
    public class ResTransactionHistoryDto
    {
        public string? transaction_history_id { get; set; }  
        public string? user_id { get; set; }
        public string? title_daily_tasks { get; set; }
        public string? title_challenge_tasks { get; set; }
        public string? reward_amount { get; set; }
        public string? type { get; set; }
        public int? status { get; set; }
        public DateTime? created_at { get; set; }
    }
}
