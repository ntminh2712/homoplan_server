using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeminarAPI.Data.Model;

namespace SeminarAPI.Mapping
{
    public class UsersMap : MappingEntityTypeConfiguration<Users>
    {
        public override void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("users");
            builder.HasKey(p => p.User_Id);
            builder.Property(p => p.User_Id);
            builder.Property(p => p.User_Name)
                .HasColumnType("varchar(50)");
            builder.Property(p => p.Password)
                .HasColumnType("varchar(50)");
            builder.Property(p => p.Phone)
                .HasColumnType("varchar(50)");
            builder.Property(p => p.Salt)
                .HasColumnType("varchar(50)");
            builder.Property(p => p.Full_Name)
                .HasColumnType("varchar(50)")
                .UseCollation("Vietnamese_CI_AS");
            builder.Property(p => p.Country)
                .HasColumnType("varchar(50)");
            builder.Property(p => p.Email)
                .HasColumnType("varchar(50)");
            builder.Property(p => p.Reference_Id)
                .HasColumnType("varchar(50)");
            builder.Property(p => p.reference_count)
                .HasColumnType("int");
            builder.Property(p => p.partner_id)
                .HasColumnType("varchar(50)");
            builder.Property(p => p.avatar)
                .HasColumnType("varchar(250)");
            builder.Property(p => p.Created_At)
                .HasColumnName("Created_at")
                .HasColumnType("Datetime");
            base.Configure(builder);
        }
    }
}
