using Microsoft.AspNetCore.Http;
using System;

namespace SeminarAPI.Data.Dto
{
    public class RankingDto
    {
        public string? ranking_id { get; set; }
        public string? user_id { get; set; }
        public string? reward_amount { get; set; }
        public string? type { get; set; }
        public int? status { get; set; }
        public DateTime? Created_At { get; set; }
    }
}
