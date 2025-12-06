using Saturday_Back.Features.Schedules.Dtos;
using static Saturday_Back.Features.Schedules.ScheduleService;

namespace Saturday_Back.Features.Schedules.Interfaces
{
    public interface IScheduleEntriesResolver
    {
        List<ScheduleEntry> ResolveEntriesAsync(ScheduleFields fields, ScheduleRequestDto request);
    }
}