using Ledighetskalkylatorn.Enums;
using Ledighetskalkylatorn.Models;
using PublicHoliday;

namespace Ledighetskalkylatorn.Services;

public class FreedomService : IFreedomService
{
    public Task<Result> GetDaysAsync(DateTime startDate, DateTime endDate)
    {
        try
        {
            if (startDate < DateTime.Now || endDate < DateTime.Now)
            {
                return Task.FromResult(new Result("Start eller slutdatum har redan passerat. Välj ett kommande datum &#x23F3;", MessageType.Warning));
            }
            if (startDate > endDate)
            {
                return Task.FromResult(new Result("Startdatum kan inte vara efter slutdatum 🚫", MessageType.Warning));
            }

            var dateRange = GetAllDates(startDate, endDate);
            if (dateRange.Count > 30)
            {
                return Task.FromResult(new Result("Testa ett mindre datumspann &#x1F448;", MessageType.Secondary));
            }

            var redDays = new List<DayOffModel>();
            var weekendDays = new List<DayOffModel>();
            var workingDays = new List<DateTime>();
            foreach (var date in dateRange)
            {
                if (IsWeekend(date, out var dayName))
                {
                    var weekend = new DayOffModel(date, dayName);
                    weekendDays.Add(weekend);
                }
                else if (IsRedDay(date, out var holidayName))
                {
                    var redDay = new DayOffModel(date, holidayName);
                    redDays.Add(redDay);
                }
                else if (IsWorkingDay(date))
                {
                    workingDays.Add(date);
                }
            }

            if (!redDays.Any() && !weekendDays.Any())
            {
                return Task.FromResult(new Result(redDays, weekendDays, workingDays, "Din sökning innehöll tyvärr inga lediga dagar &#x1F61F;", MessageType.Secondary));
            }

            return Task.FromResult(new Result(redDays, weekendDays, workingDays));
        }
        catch (Exception)
        {
            return Task.FromResult(new Result("Något gick fel! &#x1F61F;", MessageType.Danger));
        }
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

    private bool IsRedDay(DateTime date, out string description)
    {
     
        description = string.Empty;
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

    private bool IsWeekend(DateTime date, out string description)
    {
        description = string.Empty;

        if (date.DayOfWeek == DayOfWeek.Saturday)
        {
            description = "Lördag";
            return true;
        }
        else if (date.DayOfWeek == DayOfWeek.Sunday)
        {
            description = "Söndag";
            return true;
        }

        return false;
    }

    private static bool IsWorkingDay(DateTime date)
    {
        return new SwedenPublicHoliday().IsWorkingDay(date);
    }

    public Task<List<DayOffModel>> GetDaysAsync()
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