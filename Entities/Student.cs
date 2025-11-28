namespace Saturday_Back.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string? Identificator { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public int StudyYearId { get; set; }
        public StudyYear? StudyYear { get; set; }
    }
}

