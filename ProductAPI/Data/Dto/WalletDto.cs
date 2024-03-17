using System;

namespace SeminarAPI.Data.Dto
{
    public class WalletDto
    {
        public string? wallet_id { get; set; }  
        public string? user_id { get; set; }
        public string? female_usd { get; set; }
        public string? male_usd { get; set; }
        public string? amount_usd { get; set; }
        public string? daily_rewards { get; set; }
        public DateTime? created_at { get; set; }
    }
}
