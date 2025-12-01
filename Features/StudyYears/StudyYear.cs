using Saturday_Back.Features.Students;

namespace Saturday_Back.Features.StudyYears
{
    public class StudyYear
    {
        public int Id { get; set; }
        public string? YearRange { get; set; }

        // Navigation property - one StudyYear can have many Students
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}

