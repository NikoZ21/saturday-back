using Saturday_Back.Features.StudyYears;

namespace Saturday_Back.Features.BaseCosts
{
    public class BaseCost
    {
        public int Id { get; set; }
        public int StudyYearId { get; set; }
        public StudyYear? StudyYear { get; set; }
        public decimal Cost { get; set; }
    }
}

