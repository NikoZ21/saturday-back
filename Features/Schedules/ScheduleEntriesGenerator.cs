using Saturday_Back.Common.Enums;
using Saturday_Back.Features.StudyYears;

namespace Saturday_Back.Features.Schedules
{
    /// <summary>
    /// Generates payment schedule entries based on payment type and date range.
    /// Factory class responsible for creating different payment schedules.
    /// </summary>
    public class ScheduleEntriesGenerator
    {
        private readonly int paymentDay = 4;

        /// <summary>
        /// Generates a list of payment entries based on the payment type.
        /// </summary>
        /// <param name="totalCost">Total cost to be distributed across payment entries</param>
        /// <param name="paymentType">Type of payment schedule (one-time, two-part, or monthly)</param>
        /// <param name="firstMonth">Starting month (1-12)</param>
        /// <param name="lastMonth">Ending month (1-12)</param>
        /// <param name="studyYear">Study year for the payment schedule</param>
        /// <returns>List of schedule entries with dates and costs</returns>
        public List<ScheduleEntry> Generate(
            decimal totalCost,
            PaymentTypeValue paymentType,
            int firstMonth,
            int lastMonth,
            StudyYear studyYear)
        {
            var entries = paymentType switch
            {
                PaymentTypeValue.ONETIME => GenerateSinglePayment(totalCost, firstMonth, studyYear),
                PaymentTypeValue.TWOTIME => GenerateTwoPartPayment(totalCost, firstMonth, lastMonth, studyYear),
                PaymentTypeValue.MONTHLY => GenerateMonthlyPayments(totalCost, firstMonth, lastMonth, studyYear),
                _ => throw new ArgumentException($"Invalid payment type: {paymentType}", nameof(paymentType))
            };

            return entries.ToList();
        }

        /// <summary>
        /// Generates a single payment entry for one-time payment.
        /// </summary>
        private ScheduleEntry[] GenerateSinglePayment(decimal totalCost, int month, StudyYear studyYear)
            => [CreateEntry(month, studyYear, totalCost)];

        /// <summary>
        /// Generates two payment entries, splitting the cost in half.
        /// </summary>
        private ScheduleEntry[] GenerateTwoPartPayment(decimal totalCost, int firstMonth, int lastMonth, StudyYear studyYear)
        {
            var secondPaymentMonth = firstMonth + ((lastMonth - firstMonth) / 2);
            var halfCost = totalCost / 2;

            return
            [
                CreateEntry(firstMonth, studyYear, halfCost),
                CreateEntry(secondPaymentMonth, studyYear, halfCost)
            ];
        }

        /// <summary>
        /// Generates monthly payment entries, distributing the cost evenly across months.
        /// </summary>
        private ScheduleEntry[] GenerateMonthlyPayments(decimal totalCost, int firstMonth, int lastMonth, StudyYear studyYear)
        {
            var monthsCount = lastMonth - firstMonth + 1;

            if (monthsCount <= 0)
                throw new ArgumentException(
                    $"Last month ({lastMonth}) must be after first month ({firstMonth})",
                    nameof(lastMonth));

            var monthlyPayment = totalCost / monthsCount;
            var scheduleEntries = new ScheduleEntry[monthsCount];

            for (int i = 0; i < monthsCount; i++)
            {
                scheduleEntries[i] = CreateEntry(firstMonth + i, studyYear, monthlyPayment);
            }

            return scheduleEntries;
        }

        private ScheduleEntry CreateEntry(int month, StudyYear studyYear, decimal amount)
        {
            var (year, normalizedMonth) = GetYearMonth(month, studyYear);
            var day = Math.Min(paymentDay, DateTime.DaysInMonth(year, normalizedMonth));

            return new ScheduleEntry
            {
                Date = new DateTime(year, normalizedMonth, day),
                Cost = amount
            };
        }

        private (int Year, int Month) GetYearMonth(int month, StudyYear studyYear)
        {
            var year = month > 12 ? studyYear.Range.EndYear : studyYear.Range.StartYear;
            var normalizedMonth = ((month - 1) % 12) + 1;
            return (year, normalizedMonth);
        }
    }
}

