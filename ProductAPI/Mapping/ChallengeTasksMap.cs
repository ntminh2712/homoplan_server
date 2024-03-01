using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeminarAPI.Data.Model;

namespace SeminarAPI.Mapping
{
    public class ChallengeTasksMap : MappingEntityTypeConfiguration<ChallengeTasks>
    {
        public override void Configure(EntityTypeBuilder<ChallengeTasks> builder)
        {
            builder.ToTable("challenge_tasks");
            builder.HasKey(p => p.challenge_tasks_id);
            builder.Property(p => p.challenge_tasks_id);
            builder.Property(p => p.icon);
            builder.Property(p => p.Description);
            builder.Property(p => p.title);
            builder.Property(p => p.link);
            builder.Property(p => p.status);
            builder.Property(p => p.level);
            builder.Property(p => p.reward_amount);
            builder.Property(p => p.Created_At);
            base.Configure(builder);
        }
    }
}
