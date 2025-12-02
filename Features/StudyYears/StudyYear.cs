using System.ComponentModel.DataAnnotations.Schema;
using Saturday_Back.Features.Students;

namespace Saturday_Back.Features.StudyYears
{
    public class StudyYear
    {
        public int Id { get; set; }
        public YearRangeValue Range { get; private set; } = default!;

        [NotMapped] public int StartYear => Range.StartYear;
        [NotMapped] public int EndYear => Range.EndYear;
        [NotMapped] public string YearRange => Range.ToString();

        // Navigation property - one StudyYear can have many Students
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }

    public sealed record YearRangeValue(int StartYear, int EndYear)
    {
        public override string ToString() => $"{StartYear}-{EndYear}";
        public static YearRangeValue Parse(string value)
        {
            var parts = value.Split('-', StringSplitOptions.TrimEntries);
            return new YearRangeValue(int.Parse(parts[0]), int.Parse(parts[1]));
        }
    }
}

