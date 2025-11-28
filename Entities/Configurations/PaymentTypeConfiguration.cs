using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Saturday_Back.Entities.Configurations
{
    public class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.ToTable("payment_types");

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


