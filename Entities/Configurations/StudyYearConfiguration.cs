using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Saturday_Back.Entities.Configurations
{
    public class StudyYearConfiguration : IEntityTypeConfiguration<StudyYear>
    {
        public void Configure(EntityTypeBuilder<StudyYear> builder)
        {
            builder.ToTable("study_years");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("rec_id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(e => e.Name)
                .IsUnique();

            builder.HasMany(e => e.Students)
                .WithOne(s => s.StudyYear)
                .HasForeignKey(s => s.StudyYearId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

