using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeminarAPI.Data.Dto;

namespace SeminarAPI.Mapping
{
    public class TransactionHistoryMap : MappingEntityTypeConfiguration<TransactionHistory>
    {
        public override void Configure(EntityTypeBuilder<TransactionHistory> builder)
        {
            builder.ToTable("transaction_history");
            builder.HasKey(p => p.transaction_history_id);
            builder.Property(p => p.transaction_history_id);
            builder.Property(p => p.user_id);
            builder.Property(p => p.daily_tasks_id);
            builder.Property(p => p.challenge_tasks_id);
            builder.Property(p => p.reward_amount);
            builder.Property(p => p.status);
            builder.Property(p => p.type);
            builder.Property(p => p.Created_At);
            base.Configure(builder);
        }
    }
}
