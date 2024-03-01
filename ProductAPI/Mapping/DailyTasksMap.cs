using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeminarAPI.Data.Model;

namespace SeminarAPI.Mapping
{
    public class DailyTasksMap : MappingEntityTypeConfiguration<DailyTasks>
    {
        public override void Configure(EntityTypeBuilder<DailyTasks> builder)
        {
            builder.ToTable("daily_tasks");
            builder.HasKey(p => p.daily_tasks_id);
            builder.Property(p => p.daily_tasks_id);
            builder.Property(p => p.icon)
                .HasColumnType("varchar(255)");
            builder.Property(p => p.Description)
                .HasColumnType("varchar(255)");
            builder.Property(p => p.title)
                .HasColumnType("nvarchar(50)");
            builder.Property(p => p.link)
                .HasColumnType("varchar(255)");
            builder.Property(p => p.status)
                .HasColumnType("int");
            builder.Property(p => p.reward_amount)
                .HasColumnType("varchar(50)");
            builder.Property(p => p.Created_At)
                .HasColumnName("Created_at")
                .HasColumnType("Datetime");
            base.Configure(builder);
        }
    }
}
