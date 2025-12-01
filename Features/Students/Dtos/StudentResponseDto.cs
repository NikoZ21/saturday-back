namespace Saturday_Back.Features.Students.Dtos
{
    public class StudentResponseDto
    {
        public int Id { get; set; }
        public string? Identificator { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int AdmissionYearId { get; set; }
        public string? AdmissionYearRange { get; set; }
    }
}

