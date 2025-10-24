using System.ComponentModel.DataAnnotations;

namespace StudentGradingSystem.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        [Required]
        public string Name { get; set; } = null!;

        public ICollection<Student>? StudentGrades { get; set; }
    }
}
