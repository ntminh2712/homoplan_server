using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeminarAPI.Data.Model;

namespace SeminarAPI.Mapping
{
    public class WalletMap : MappingEntityTypeConfiguration<Wallet>
    {
        public override void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("wallet");
            builder.HasKey(p => p.wallet_id);
            builder.Property(p => p.wallet_id);
            builder.Property(p => p.user_id);
            builder.Property(p => p.female_usd);
            builder.Property(p => p.male_usd);
            builder.Property(p => p.amount_usd);
            builder.Property(p => p.Created_At)
                .HasColumnName("Created_at")
                .HasColumnType("Datetime");
            base.Configure(builder);
        }
    }
}
