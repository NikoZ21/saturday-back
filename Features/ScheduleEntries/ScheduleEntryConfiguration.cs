using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saturday_Back.Features.Schedules;

namespace Saturday_Back.Features.ScheduleEntries
{
    public class ScheduleEntryConfiguration : IEntityTypeConfiguration<ScheduleEntry>
    {
        public void Configure(EntityTypeBuilder<ScheduleEntry> entity)
        {
            entity.ToTable("schedule_entries");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasColumnName("rec_id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Date)
                .HasColumnName("date")
                .HasColumnType("VARCHAR(10)")
                .IsRequired();

            entity.Property(e => e.Amount)
                .HasColumnName("amount")
                .HasPrecision(10, 2)
                .IsRequired();

            // Foreign key to Schedule
            entity.Property(e => e.ScheduleId)
                .HasColumnName("schedule_id")
                .IsRequired();

            // Relationship with Schedule (one-to-many)
            entity.HasOne(e => e.Schedule)
                .WithMany(s => s.ScheduleEntries)
                .HasForeignKey(e => e.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

