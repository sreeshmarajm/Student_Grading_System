using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using StudentGradingSystem.Models;

namespace StudentGradingSystem.Data
{
    public class StudentGradeDbContext:DbContext
    {
        public StudentGradeDbContext(DbContextOptions<StudentGradeDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed sample data (optional)
            

            modelBuilder.Entity<Subject>().HasData(
                new Subject { SubjectId = 1, Name = "Mathematics" },
                new Subject { SubjectId = 2, Name = "English" }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, StudentName = "Nimal", SubjectId = 1, Grade = 80 },
                new Student { StudentId = 2, StudentName = "Alice", SubjectId = 2, Grade = 72 },
                new Student { StudentId = 3, StudentName = "Mathew", SubjectId = 1, Grade = 60 }
            );
        }
    }
}
