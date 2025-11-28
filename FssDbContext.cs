using Microsoft.EntityFrameworkCore;
using Saturday_Back.Entities;
using System.Reflection;

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
        public DbSet<Student> Students { get; set; }
        public DbSet<StudyYear> StudyYears { get; set; }
        public DbSet<Schedule> Schedules { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all entity configurations from the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Apply lowercase naming convention to all tables and columns
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
        }
    }
}
