using Ledighetskalkylatorn.Models;
using PublicHoliday;

namespace Ledighetskalkylatorn.Services;

public class FreedomService : IFreedomService
{
    public Task<Result> GetDaysAsync(DateTime startDate, DateTime endDate)
    {
        var daysOff = new List<DayOffModel>();
        var workingDays = new List<DateTime>();

        var dateRange = GetAllDates(startDate, endDate);
        foreach (var date in dateRange)
        {
            if (IsDayOff(date, out var description))
            {
                var redDay = new DayOffModel(date, description);
                daysOff.Add(redDay);
            }
            if (IsWorkingDay(date))
            {
                workingDays.Add(date);
            }
        }

        return Task.FromResult(new Result(daysOff, workingDays));
    }

    private List<DateTime> GetAllDates(DateTime startDate, DateTime endDate)
    {
        List<DateTime> range = new();

        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
        {
            range.Add(date);
        }

        return range;
    }

    private bool IsDayOff(DateTime date, out string description)
    {
     
        description = string.Empty;

        if (date.DayOfWeek == DayOfWeek.Saturday)
        {
            description = "Lördag";
            return true;
        }
        else if(date.DayOfWeek == DayOfWeek.Sunday)
        {
            description = "Söndag";
            return true;
        }

        bool isHoliday = new SwedenPublicHoliday().IsPublicHoliday(date);
        if (isHoliday)
        {

            description = new SwedenPublicHoliday().PublicHolidayNames(date.Year)
                        .Where(x => x.Key.Date == date.Date)
                        .Select(x => x.Value)
                        .FirstOrDefault() ?? string.Empty;

            return true;
        }

        return false;
    }

    private static bool IsWorkingDay(DateTime date)
    {
        return new SwedenPublicHoliday().IsWorkingDay(date);
    }

    public Task<List<DayOffModel>> GetRedDaysAsync()
    {
        var daysOff = new List<DayOffModel>();
        var holidays = new SwedenPublicHoliday().PublicHolidays(DateTime.Now.Year).Where(x => x.Date >= DateTime.Now);

        foreach (var holiday in holidays)
        {
            var description = new SwedenPublicHoliday().PublicHolidayNames(DateTime.Now.Year)
                       .Where(x => x.Key.Date == holiday.Date.Date)
                       .Select(x => x.Value)
                       .FirstOrDefault() ?? string.Empty;

            daysOff.Add(new DayOffModel(holiday.Date, description));

        }

        return Task.FromResult(daysOff);
    }
}