using SeminarAPI.Data.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarAPI.Data.Dto
{
    [Table("transaction_history")]
    public class TransactionHistory: BaseEntity
    {
        [Key]
        public string? transaction_history_id { get; set; }
        public string? user_id { get; set; }
        public string? daily_tasks_id { get; set; }
        public string? challenge_tasks_id { get; set; }
        public string? reward_amount { get; set; }
        public string? type { get; set; } // 1 la nhiem vu, 2 la gioi thieu, 3 la thu thach
        public int? status { get; set; }  // 0 đã tính điểm, 1 chưa tính điểm
    }
}
