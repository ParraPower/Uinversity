using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UniversityCore.Models.Config;
using UniversityData.Entites;
using UniversityData.EntityConfiguration;

namespace UniversityData.Context
{
    public class UniversityContext : DbContext
    {
        private readonly string connectionString;

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        public UniversityContext() : base()
        {
            connectionString = string.Empty;
        }  

        public UniversityContext(DbContextOptions dbContextOptions, IOptions<SqlOptions> sqlOptions) : base(dbContextOptions)
        {
            connectionString = sqlOptions.Value.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new StudentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CourseEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ErollmentEntityConfiguration());
        }
    }

    
}
