using System.ComponentModel.DataAnnotations.Schema;

namespace Saturday_Back.Features.AcademicYears
{
    public class AcademicYear
    {
        public int Id { get; set; }
        public YearRangeValue Range { get; private set; } = default!;
        public decimal Cost { get; set; }

        [NotMapped] public int StartYear => Range.StartYear;
        [NotMapped] public int EndYear => Range.EndYear;
        [NotMapped] public string YearRange => Range.ToString();
    }

    public sealed record YearRangeValue(int StartYear, int EndYear)
    {
        public override string ToString() => $"{StartYear}-{EndYear}";

        public static YearRangeValue Parse(string value)
        {
            var parts = value.Split('-', StringSplitOptions.TrimEntries);
            if (parts.Length != 2)
                throw new FormatException($"Invalid academic year range '{value}'. Expected format 'YYYY-YYYY'.");

            return new YearRangeValue(int.Parse(parts[0]), int.Parse(parts[1]));
        }
    }
}

