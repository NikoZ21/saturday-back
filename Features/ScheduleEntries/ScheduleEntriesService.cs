using System.Collections.Generic;
using Saturday_Back.Common.Enums;
using Saturday_Back.Common.Exceptions;
using Saturday_Back.Common.Repositories;
using Saturday_Back.Features.AcademicYears;
using Saturday_Back.Features.BenefitTypes;
using Saturday_Back.Features.PaymentTypes;

namespace Saturday_Back.Features.ScheduleEntries
{
    public class ScheduleEntriesService(IRepository<ScheduleEntry> scheduleEntryRepository)
    {
        private readonly IRepository<ScheduleEntry> _scheduleEntryRepository = scheduleEntryRepository;
        private readonly int paymentDay = 4;


        public async Task<List<ScheduleEntry>> GetAllByScheduleIdAsync(int scheduleId)
        {
            return await _scheduleEntryRepository.WhereAsync(e => e.ScheduleId == scheduleId);
        }

        public async Task<List<ScheduleEntry>> CreateAsync(int recId, PaymentType paymentType, BenefitType benefitType, AcademicYear academicYear, int firstSaturday, int lastSaturday, int firstMonth, int lastMonth)
        {
            var cost = CalculateCost(academicYear.Cost, firstSaturday, lastSaturday, paymentType, benefitType);

            var entries = paymentType.Value switch
            {
                PaymentTypeValue.ONETIME => GenerateSinglePayment(cost, firstMonth, academicYear),
                PaymentTypeValue.TWOTIME => GenerateTwoPartPayment(cost, firstMonth, lastMonth, academicYear),
                PaymentTypeValue.MONTHLY => GenerateMonthlyPayments(cost, firstMonth, lastMonth, academicYear),
                _ => throw new BusinessRuleException($"Invalid payment type: {paymentType}" + $" {nameof(paymentType)}")
            };

            List<ScheduleEntry> addedEntries = [];

            foreach (var entry in entries)
            {
                entry.ScheduleId = recId;
                var addedEntry = await _scheduleEntryRepository.AddAsync(entry);
                addedEntries.Add(addedEntry);
            }
            return addedEntries;
        }
        /// <summary>
        /// Generates a single payment entry for one-time payment.
        /// </summary>
        private ScheduleEntry[] GenerateSinglePayment(decimal totalCost, int month, AcademicYear academicYear)
            => [CreateEntry(month, academicYear, totalCost)];

        /// <summary>
        /// Generates two payment entries, splitting the cost in half.
        /// </summary>
        private ScheduleEntry[] GenerateTwoPartPayment(decimal totalCost, int firstMonth, int lastMonth, AcademicYear academicYear)
        {
            var secondPaymentMonth = firstMonth + ((lastMonth - firstMonth) / 2);
            var halfCost = totalCost / 2;

            return
            [
                CreateEntry(firstMonth, academicYear, halfCost),
                CreateEntry(secondPaymentMonth, academicYear, halfCost)
            ];
        }

        /// <summary>
        /// Generates monthly payment entries, distributing the cost evenly across months.
        /// </summary>
        private ScheduleEntry[] GenerateMonthlyPayments(decimal totalCost, int firstMonth, int lastMonth, AcademicYear academicYear)
        {
            var monthsCount = lastMonth - firstMonth;
            Console.WriteLine("months count {0}", monthsCount);

            if (monthsCount <= 0)
                throw new BusinessRuleException(
                    $"Last month ({lastMonth}) must be after first month ({firstMonth})");

            var monthlyPayment = totalCost / monthsCount;
            Console.WriteLine("monthly payment {0}", monthlyPayment);
            var scheduleEntries = new ScheduleEntry[monthsCount];

            for (int i = 0; i < monthsCount; i++)
            {
                scheduleEntries[i] = CreateEntry(firstMonth + i, academicYear, monthlyPayment);
            }

            return scheduleEntries;
        }

        private ScheduleEntry CreateEntry(int month, AcademicYear academicYear, decimal amount)
        {
            var (year, normalizedMonth) = GetYearMonth(month, academicYear);
            var day = Math.Min(paymentDay, DateTime.DaysInMonth(year, normalizedMonth));

            return new ScheduleEntry
            {
                Date = new DateOnly(year, normalizedMonth, day).ToString("yyyy-MM-dd"),
                Amount = amount
            };
        }

        private static (int Year, int Month) GetYearMonth(int month, AcademicYear academicYear)
        {
            var year = month > 12 ? academicYear.Range.EndYear : academicYear.Range.StartYear;
            var normalizedMonth = ((month - 1) % 12) + 1;
            return (year, normalizedMonth);
        }

        private static decimal CalculateCost(decimal amount, int firstSaturday, int lastSaturday, PaymentType paymentType, BenefitType benefitType)
        {
            if (amount == 0)
            {
                throw new BusinessRuleException("Amount is 0. Please ensure the amount is set correctly.");
            }

            decimal saturdaysCount = lastSaturday - firstSaturday + 1;
            var totalDiscount = benefitType.Discount + (paymentType.Discount ?? 0);
            var discountedCost = amount * (1 - totalDiscount / 100);
            var result = discountedCost * (saturdaysCount / 30);

            return result;
        }
    }
}