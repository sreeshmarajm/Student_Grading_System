namespace StudentGradingSystem.Models
{
    public class StudentGradeViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = null!;
        public string SubjectName { get; set; } = null!;
        public int Grade { get; set; }
        public string Remarks { get; set; } = null!;
    }
}
