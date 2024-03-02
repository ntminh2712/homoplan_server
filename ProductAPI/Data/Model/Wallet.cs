using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarAPI.Data.Model
{
    [Table("wallet")]
    public class Wallet : BaseEntity
    {
        [Key]
        public string? wallet_id { get; set; }
        public string? user_id { get; set; }
        public string? female_usd { get; set; }
        public string? male_usd { get; set; }
        public string? amount_usd { get; set; }
    }
}
