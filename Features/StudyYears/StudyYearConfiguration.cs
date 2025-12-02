using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Saturday_Back.Features.Students;

namespace Saturday_Back.Features.StudyYears
{
    public class StudyYearConfiguration : IEntityTypeConfiguration<StudyYear>
    {
        public void Configure(EntityTypeBuilder<StudyYear> builder)
        {
            builder.ToTable("study_years", t =>
            {
                // Enforce "YYYY-YYYY" format at database level (MySQL syntax)
                t.HasCheckConstraint("CK_StudyYear_YearRange_Format", "`yearrange` REGEXP '^[0-9]{4}-[0-9]{4}$'");
            });

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("rec_id")
                .ValueGeneratedOnAdd();

            var converter = new ValueConverter<YearRangeValue, string>(
                    v => v.ToString(),
                    v => YearRangeValue.Parse(v));

            builder.Property(e => e.Range)
                   .HasColumnName("year_range")
                   .HasConversion(converter)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.HasMany(e => e.Students)
                .WithOne(s => s.AdmissionYear)
                .HasForeignKey(s => s.AdmissionYearId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

