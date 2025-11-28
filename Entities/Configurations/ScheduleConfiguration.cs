using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Saturday_Back.Entities.Configurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> entity)
        {
            entity.ToTable("schedules");

            // Primary key
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Id)
                  .HasColumnName("rec_id")
                  .ValueGeneratedOnAdd();

            // Relationships
            entity.HasOne(s => s.Subject)
                  .WithMany()
                  .HasForeignKey(s => s.SubjectId);

            entity.HasOne(s => s.PaymentType)
                  .WithMany()
                  .HasForeignKey(s => s.PaymentTypeId);

            entity.HasOne(s => s.BenefitType)
                  .WithMany()
                  .HasForeignKey(s => s.BenefitTypeId);

            entity.HasOne(s => s.BaseCost)
                  .WithMany()
                  .HasForeignKey(s => s.BaseCostId);

            // Normal INT range columns
            entity.Property(s => s.FirstSaturday).IsRequired();
            entity.Property(s => s.LastSaturday).IsRequired();
            entity.Property(s => s.FirstMonth).IsRequired();
            entity.Property(s => s.LastMonth).IsRequired();

            // 🔥 Convert ScheduleEntries list → JSON column in MySQL
            entity.Property(s => s.ScheduleEntries)
                  .HasConversion(
                      v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                      v => JsonSerializer.Deserialize<List<ScheduleEntry>>(v, (JsonSerializerOptions?)null) ?? new()
                  )
                  .HasColumnName("schedule_entries")
                  .HasColumnType("json");
        }
    }
}


