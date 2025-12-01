using Saturday_Back.Features.StudyYears;

namespace Saturday_Back.Features.Students
{
    public class Student
    {
        public int Id { get; set; }
        public string? Identificator { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int AdmissionYearId { get; set; }
        public StudyYear? AdmissionYear { get; set; }
    }
}

