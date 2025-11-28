namespace Saturday_Back.Entities
{
    public class StudyYear
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Student> Students { get; set; } = [];
    }
}

