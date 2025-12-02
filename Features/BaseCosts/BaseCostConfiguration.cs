using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Saturday_Back.Features.BaseCosts
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

            builder.Property(e => e.StudyYearId)
                .HasColumnName("study_year_id")
                .IsRequired();

            builder.HasOne(e => e.StudyYear)
                .WithMany()
                .HasForeignKey(e => e.StudyYearId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Cost)
                .HasColumnName("cost")
                .HasPrecision(10, 2)
                .IsRequired();
        }
    }
}

