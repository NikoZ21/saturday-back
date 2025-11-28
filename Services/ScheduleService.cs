using Saturday_Back.Dtos;
using Saturday_Back.Entities;
using Saturday_Back.Enums;
using Saturday_Back.Repositories;

public class ScheduleService(ICachedRepository<Schedule, ScheduleResponseDto> repository)
{
    private readonly ICachedRepository<Schedule, ScheduleResponseDto> _repository = repository;

    public string BuildPaymentSchedule(BenefitType benefitType, PaymentType paymentType, int baseCost, int firstSaturday, int lastSaturday, int firstMonth, int lastMonth)
    {

        if (lastMonth < firstMonth)
        {
            throw new ArgumentOutOfRangeException(nameof(lastMonth), "Last month can't be less than first month");
        }

        if (lastSaturday < firstSaturday)
        {
            throw new ArgumentOutOfRangeException(nameof(lastSaturday), "Last Saturday can't be less than first Saturday");
        }

        var saturdaysCount = lastSaturday - firstSaturday + 1;
        var totalDiscount = benefitType.Discount + (paymentType.Discount ?? 0);
        var discountedCost = baseCost * (1 - totalDiscount / 100);
        var cost = discountedCost * (saturdaysCount / 30);

        // Route to the appropriate schedule generator based on payment type
        var scheduleEntries = paymentType.Value switch
        {
            PaymentTypeValue.ONETIME => GenerateSinglePaymentSchedule(cost, firstMonth, lastMonth),
            PaymentTypeValue.TWOTIME => GenerateTwoPartPaymentSchedule(cost, firstMonth, lastMonth),
            PaymentTypeValue.MONTHLY => GenerateMonthlySchedule(cost, firstMonth, lastMonth),
            _ => throw new ArgumentException("Invalid payment type")
        };

        // var schedule = new Schedule{

        // }

        return "Successfully generated schedule";
    }

    private ScheduleEntry[] GenerateSinglePaymentSchedule(decimal totalCost, int firstMonth, int lastMonth)
    {
        // Single payment - all at once
        var date = new DateTime(2025, firstMonth, 1);

        return new ScheduleEntry[] { new ScheduleEntry { Date = date, Cost = totalCost } };
    }
    private ScheduleEntry[] GenerateTwoPartPaymentSchedule(decimal totalCost, int firstMonth, int lastMonth)
    {
        // Two payments - split the cost
        var halfCost = totalCost / 2;

        var firstPaymentDate = new DateTime(2025, firstMonth, 1);
        int secondPaymentMonth = (lastMonth - firstMonth) / 2;
        var secondPaymentDate = new DateTime(2025, secondPaymentMonth, 1);

        return new ScheduleEntry[]
        {
            new ScheduleEntry { Date = firstPaymentDate, Cost = halfCost },
            new ScheduleEntry { Date = secondPaymentDate, Cost = halfCost }
        };
    }

    private ScheduleEntry[] GenerateMonthlySchedule(decimal totalCost, int firstMonth, int lastMonth)
    {
        // Monthly payments across the period
        var monthsCount = lastMonth - firstMonth;
        var monthlyPayment = totalCost / monthsCount;

        var scheduleEntries = new ScheduleEntry[monthsCount];
        for (int i = 0; i < monthsCount; i++)
        {
            var month = (firstMonth + i) % 12;

            if (month == 0)
            {
                month = 12;
            }

            scheduleEntries[i] = new ScheduleEntry { Date = new DateTime(2025, month, 1), Cost = monthlyPayment };
        }

        return scheduleEntries;
    }
}