using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Saturday_Back.Features.BenefitTypes
{
    public class BenefitTypeConfiguration : IEntityTypeConfiguration<BenefitType>
    {
        public void Configure(EntityTypeBuilder<BenefitType> builder)
        {
            builder.ToTable("benefit_types");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("rec_id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Value)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Discount)
                .HasPrecision(5, 2)
                .IsRequired();
        }
    }
}
