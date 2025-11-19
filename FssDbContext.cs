using Microsoft.EntityFrameworkCore;
using Saturday_Back.Entities;

namespace Saturday_Back
{
    public class FssDbContext : DbContext
    {
        public FssDbContext(DbContextOptions<FssDbContext> options) : base(options)
        {
        }

        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<BenefitType> BenefitTypes { get; set; }
        public DbSet<BaseCost> BaseCosts { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity?.GetTableName()?.ToLowerInvariant());

                if (entity == null)
                {
                    continue;
                }

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToLowerInvariant());
                }
            }

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.ToTable("payment_types");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("rec_id").ValueGeneratedOnAdd();

                entity.Property(e => e.Value).HasConversion<string>().IsRequired();

                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();

                entity.Property(e => e.Discount).HasPrecision(5, 2).IsRequired();
            });


            modelBuilder.Entity<BenefitType>(entity =>
            {
                entity.ToTable("benefit_types");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("rec_id").ValueGeneratedOnAdd();

                entity.Property(e => e.Value).HasConversion<string>().IsRequired();

                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();

                entity.Property(e => e.Discount).HasPrecision(5, 2).IsRequired();
            });


            modelBuilder.Entity<BaseCost>(entity =>
            {
                entity.ToTable("base_costs");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("rec_id").ValueGeneratedOnAdd();

                entity.Property(e => e.StudyYear).HasColumnName("study_year").HasMaxLength(20).IsRequired();
                entity.HasIndex(e => e.StudyYear).IsUnique();

                entity.Property(e => e.Cost).HasColumnName("cost").HasPrecision(10, 2).IsRequired();
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subjects");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("rec_id").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.HasIndex(e => e.Name).IsUnique();
            });
        }
    }
}
