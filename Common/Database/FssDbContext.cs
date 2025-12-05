using Microsoft.EntityFrameworkCore;
using Saturday_Back.Features.AcademicYears;
using Saturday_Back.Features.BenefitTypes;
using Saturday_Back.Features.PaymentTypes;
using Saturday_Back.Features.Schedules;
using Saturday_Back.Features.Students;
using Saturday_Back.Features.Subjects;
using System.Reflection;

namespace Saturday_Back.Common.Database
{
    public class FssDbContext : DbContext
    {
        public FssDbContext(DbContextOptions<FssDbContext> options) : base(options)
        {
        }

        // DbSets for all features
        public DbSet<PaymentType> PaymentTypes => Set<PaymentType>();
        public DbSet<BenefitType> BenefitTypes => Set<BenefitType>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<AcademicYear> AcademicYears => Set<AcademicYear>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Schedule> Schedules => Set<Schedule>();

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
