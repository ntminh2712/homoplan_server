using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeminarAPI.Data.Model;

namespace SeminarAPI.Mapping
{
    public class RankingMap : MappingEntityTypeConfiguration<Ranking>
    {
        public override void Configure(EntityTypeBuilder<Ranking> builder)
        {
            builder.ToTable("ranking");
            builder.HasKey(p => p.ranking_id);
            builder.Property(p => p.ranking_id);
            builder.Property(p => p.user_id);
            builder.Property(p => p.reward_amount);
            builder.Property(p => p.type);
            builder.Property(p => p.status);
            builder.Property(p => p.Created_At);
            base.Configure(builder);
        }
    }
}
