using Saturday_Back.Common.Enums;

namespace Saturday_Back.Features.Schedules
{
    /// <summary>
    /// Generates payment schedule entries based on payment type and date range.
    /// Factory class responsible for creating different payment schedules.
    /// </summary>
    public class ScheduleEntriesGenerator
    {
        /// <summary>
        /// Generates a list of payment entries based on the payment type.
        /// </summary>
        /// <param name="totalCost">Total cost to be distributed across payment entries</param>
        /// <param name="paymentType">Type of payment schedule (one-time, two-part, or monthly)</param>
        /// <param name="firstMonth">Starting month (1-12)</param>
        /// <param name="lastMonth">Ending month (1-12)</param>
        /// <param name="year">Year for the payment schedule (defaults to 2025)</param>
        /// <returns>List of schedule entries with dates and costs</returns>
        public List<ScheduleEntry> Generate(
            decimal totalCost,
            PaymentTypeValue paymentType,
            int firstMonth,
            int lastMonth,
            int year = 2025)
        {
            var entries = paymentType switch
            {
                PaymentTypeValue.ONETIME => GenerateSinglePayment(totalCost, firstMonth, year),
                PaymentTypeValue.TWOTIME => GenerateTwoPartPayment(totalCost, firstMonth, lastMonth, year),
                PaymentTypeValue.MONTHLY => GenerateMonthlyPayments(totalCost, firstMonth, lastMonth, year),
                _ => throw new ArgumentException($"Invalid payment type: {paymentType}", nameof(paymentType))
            };

            return entries.ToList();
        }

        /// <summary>
        /// Generates a single payment entry for one-time payment.
        /// </summary>
        private ScheduleEntry[] GenerateSinglePayment(decimal totalCost, int month, int year)
        {
            var date = new DateTime(year, month, 1);
            return [new ScheduleEntry { Date = date, Cost = totalCost }];
        }

        /// <summary>
        /// Generates two payment entries, splitting the cost in half.
        /// </summary>
        private ScheduleEntry[] GenerateTwoPartPayment(decimal totalCost, int firstMonth, int lastMonth, int year)
        {
            var halfCost = totalCost / 2;
            var firstPaymentDate = new DateTime(year, firstMonth, 1);

            // Calculate middle month between first and last
            int secondPaymentMonth = firstMonth + ((lastMonth - firstMonth) / 2);
            if (secondPaymentMonth > 12)
                secondPaymentMonth = 12;
            if (secondPaymentMonth < 1)
                secondPaymentMonth = 1;

            var secondPaymentDate = new DateTime(year, secondPaymentMonth, 1);

            return [
                new ScheduleEntry { Date = firstPaymentDate, Cost = halfCost },
                new ScheduleEntry { Date = secondPaymentDate, Cost = halfCost }
            ];
        }

        /// <summary>
        /// Generates monthly payment entries, distributing the cost evenly across months.
        /// </summary>
        private ScheduleEntry[] GenerateMonthlyPayments(decimal totalCost, int firstMonth, int lastMonth, int year)
        {
            var monthsCount = lastMonth - firstMonth;

            if (monthsCount <= 0)
                throw new ArgumentException(
                    $"Last month ({lastMonth}) must be after first month ({firstMonth})",
                    nameof(lastMonth));

            var monthlyPayment = totalCost / monthsCount;
            var scheduleEntries = new ScheduleEntry[monthsCount];

            for (int i = 0; i < monthsCount; i++)
            {
                var month = (firstMonth + i) % 12;
                if (month == 0) month = 12;

                scheduleEntries[i] = new ScheduleEntry
                {
                    Date = new DateTime(year, month, 1),
                    Cost = monthlyPayment
                };
            }

            return scheduleEntries;
        }
    }
}

