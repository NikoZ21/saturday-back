using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Saturday_Back.Entities.Configurations
{
    public class StudyYearConfiguration : IEntityTypeConfiguration<StudyYear>
    {
        public void Configure(EntityTypeBuilder<StudyYear> builder)
        {
            builder.ToTable("study_years", t =>
            {
                // Enforce "YYYY-YYYY" format at database level
                t.HasCheckConstraint("CK_StudyYear_YearRange_Format", "\"YearRange\" ~ '^[0-9]{4}-[0-9]{4}$'");
            });

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("rec_id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.YearRange)
                .HasMaxLength(9)
                .IsRequired()
                .HasColumnType("varchar(9)");

            builder.HasIndex(e => e.YearRange)
                .IsUnique();

            builder.HasMany(e => e.Students)
                .WithOne(s => s.AdmissionYear)
                .HasForeignKey(s => s.AdmissionYearId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

