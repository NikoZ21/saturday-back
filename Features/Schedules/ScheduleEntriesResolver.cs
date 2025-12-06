using Saturday_Back.Common.Enums;
using Saturday_Back.Features.AcademicYears;
using Saturday_Back.Features.Schedules.Dtos;
using Saturday_Back.Features.BenefitTypes;
using Saturday_Back.Features.PaymentTypes;
using static Saturday_Back.Features.Schedules.ScheduleService;
using Saturday_Back.Common.Exceptions;
using Saturday_Back.Features.Schedules.Interfaces;

namespace Saturday_Back.Features.Schedules
{
    /// <summary>
    /// Generates payment schedule entries based on payment type and date range.
    /// Factory class responsible for creating different payment schedules.
    /// </summary>
    public class ScheduleEntriesResolver : IScheduleEntriesResolver
    {
        private readonly int paymentDay = 4;

        /// <summary>
        /// Generates a list of payment entries based on the payment type.
        /// </summary>
        /// <param name="academicYear">Academic year for the payment schedule</param>
        /// <param name="paymentType">Type of payment schedule (one-time, two-part, or monthly)</param>
        /// <param name="firstMonth">Starting month (1-12)</param>
        /// <param name="lastMonth">Ending month (1-12)</param>
        /// <returns>List of schedule entries with dates and costs</returns>
        public List<ScheduleEntry> ResolveEntriesAsync(
           ScheduleFields fields, ScheduleRequestDto request
            )
        {

            var firstMonth = request.FirstMonth;
            var lastMonth = request.LastMonth;
            var academicYear = fields.Student.AdmissionYear!;
            var paymentType = fields.PaymentType.Value;

            var totalCost = CalculateCost(fields.Student.AdmissionYear!.Cost, request.FirstSaturday, request.LastSaturday, fields.BenefitType, fields.PaymentType);

            var entries = fields.PaymentType.Value switch
            {
                PaymentTypeValue.ONETIME => GenerateSinglePayment(totalCost, firstMonth, academicYear),
                PaymentTypeValue.TWOTIME => GenerateTwoPartPayment(totalCost, firstMonth, lastMonth, academicYear),
                PaymentTypeValue.MONTHLY => GenerateMonthlyPayments(totalCost, firstMonth, lastMonth, academicYear),
                _ => throw new BusinessRuleException($"Invalid payment type: {paymentType}" + $" {nameof(paymentType)}")
            };

            return entries.ToList();
        }

        /// <summary>
        /// Generates a single payment entry for one-time payment.
        /// </summary>
        private ScheduleEntry[] GenerateSinglePayment(decimal totalCost, int month, AcademicYear academicYear)
            => [CreateEntry(1, month, academicYear, totalCost)];

        /// <summary>
        /// Generates two payment entries, splitting the cost in half.
        /// </summary>
        private ScheduleEntry[] GenerateTwoPartPayment(decimal totalCost, int firstMonth, int lastMonth, AcademicYear academicYear)
        {
            var secondPaymentMonth = firstMonth + ((lastMonth - firstMonth) / 2);
            var halfCost = totalCost / 2;

            return
            [
                CreateEntry(1, firstMonth, academicYear, halfCost),
                CreateEntry(2, secondPaymentMonth, academicYear, halfCost)
            ];
        }

        /// <summary>
        /// Generates monthly payment entries, distributing the cost evenly across months.
        /// </summary>
        private ScheduleEntry[] GenerateMonthlyPayments(decimal totalCost, int firstMonth, int lastMonth, AcademicYear academicYear)
        {
            var monthsCount = lastMonth - firstMonth;

            if (monthsCount <= 0)
                throw new BusinessRuleException(
                    $"Last month ({lastMonth}) must be after first month ({firstMonth})");

            var monthlyPayment = totalCost / monthsCount;
            var scheduleEntries = new ScheduleEntry[monthsCount];

            for (int i = 0; i < monthsCount; i++)
            {
                scheduleEntries[i] = CreateEntry(i + 1, firstMonth + i, academicYear, monthlyPayment);
            }

            return scheduleEntries;
        }

        private ScheduleEntry CreateEntry(int id, int month, AcademicYear academicYear, decimal amount)
        {
            var (year, normalizedMonth) = GetYearMonth(month, academicYear);
            var day = Math.Min(paymentDay, DateTime.DaysInMonth(year, normalizedMonth));

            return new ScheduleEntry
            {
                Id = id,
                Date = new DateTime(year, normalizedMonth, day),
                Cost = amount
            };
        }

        private static (int Year, int Month) GetYearMonth(int month, AcademicYear academicYear)
        {
            var year = month > 12 ? academicYear.Range.EndYear : academicYear.Range.StartYear;
            var normalizedMonth = ((month - 1) % 12) + 1;
            return (year, normalizedMonth);
        }

        private static decimal CalculateCost(decimal cost, int firstSaturday, int lastSaturday, BenefitType benefitType, PaymentType paymentType)
        {
            if (cost == 0)
            {
                throw new BusinessRuleException("Base cost is 0. Please ensure the base cost is set correctly.");
            }

            var saturdaysCount = lastSaturday - firstSaturday + 1;
            var totalDiscount = benefitType.Discount + (paymentType.Discount ?? 0);
            var discountedCost = cost * (1 - totalDiscount / 100);

            return discountedCost * (saturdaysCount / 30);
        }
    }
}

