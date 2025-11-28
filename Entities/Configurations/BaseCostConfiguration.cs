using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Saturday_Back.Entities.Configurations
{
    public class BaseCostConfiguration : IEntityTypeConfiguration<BaseCost>
    {
        public void Configure(EntityTypeBuilder<BaseCost> builder)
        {
            builder.ToTable("base_costs");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("rec_id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.StudyYear)
                .HasColumnName("study_year")
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(e => e.StudyYear)
                .IsUnique();

            builder.Property(e => e.Cost)
                .HasColumnName("cost")
                .HasPrecision(10, 2)
                .IsRequired();
        }
    }
}

