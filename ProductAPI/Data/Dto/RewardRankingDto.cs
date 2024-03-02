using Microsoft.AspNetCore.Http;
using System;

namespace SeminarAPI.Data.Dto
{
    public class RewardRankingDto
    {
        public string? user_id { get; set; }
        public string? reward_amount { get; set; }
    }
}
