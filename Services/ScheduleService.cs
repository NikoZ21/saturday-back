using Saturday_Back.Entities;

public class ScheduleService
{
    public string[] BuildPaymentSchedule(BenefitType benefitType, PaymentType paymentType, int baseCost, int saturdaysCount, int firstMonth, int lastMonth)
    {
        var totalDiscount = (benefitType.Discount ?? 0) + (paymentType.Discount ?? 0);
        var discountedCost = baseCost * (1 - totalDiscount / 100);
        var cost = discountedCost * (saturdaysCount / 30);
        var monthsCount = lastMonth - firstMonth;
        var monthlyPayment = cost / monthsCount;

        return new[]
        {
            "1", "2", "3", "4", "5",
        };
    }

    private void GenerateSinglePaymentSchedule() { }
    private void GenerateTwoPartPaymentSchedule() { }
    private void GenerateMonthlySchedule() { }
}