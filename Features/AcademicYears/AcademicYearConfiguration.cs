using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Saturday_Back.Features.AcademicYears
{
    public class AcademicYearConfiguration : IEntityTypeConfiguration<AcademicYear>
    {
        public void Configure(EntityTypeBuilder<AcademicYear> builder)
        {
            builder.ToTable("academic_years", t =>
            {
                t.HasCheckConstraint("CK_AcademicYear_YearRange_Format", "`year_range` REGEXP '^[0-9]{4}-[0-9]{4}$'");
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
                .HasMaxLength(9)
                .IsRequired();

            builder.HasIndex(e => e.Range)
                .IsUnique();

            builder.Property(e => e.Cost)
                .HasColumnName("cost")
                .HasPrecision(10, 2)
                .IsRequired();
        }
    }
}

