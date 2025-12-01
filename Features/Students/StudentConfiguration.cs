using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Saturday_Back.Features.Students
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("students");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("rec_id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Identificator)
                .HasMaxLength(11)
                .IsRequired();

            builder.HasIndex(e => e.Identificator)
                .IsUnique();

            builder.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(e => e.AdmissionYear)
                .WithMany(sy => sy.Students)
                .HasForeignKey(e => e.AdmissionYearId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

