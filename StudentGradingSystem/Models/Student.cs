using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentGradingSystem.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        public string StudentName { get; set; } = string.Empty;

        [Required]
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }

        [Range(0, 100)]
        public int Grade { get; set; }
       
        [NotMapped]
        public string Remarks => Grade >= 75 ? "PASS" : "FAIL";
    }
}
