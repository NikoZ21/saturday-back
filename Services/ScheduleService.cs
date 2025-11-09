using Saturday_Back.Entities;

namespace Saturday_Back.Services
{
    public class ScheduleService
    {
        public string[] GenerateSchedule(BenefitType benefitType, BenefitType scheduleType, int baseCost, int saturadysCount, int firstMonth, int lastMonth)
        {
            // Calculate total discounted price
            var totalDiscount = (benefitType.Discount ?? 0) + (scheduleType.Discount ?? 0);
            var discountedCost = baseCost * (1 - totalDiscount / 100);

            // Caluclate discounted price for saturdays
            var cost = discountedCost * (saturadysCount / 30);


            // calculate monthly payment except last month
            var monthsCount = lastMonth - firstMonth;
            var monthlyPayment = cost / monthsCount;


            return new string[]
            {
               "1",
               "2",
               "3",
               "4",
               "5",
            };
        }
    }
}
