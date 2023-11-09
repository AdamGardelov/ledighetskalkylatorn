using Ledighetskalkylatorn.Services;
using System.Text;

public class CalendarService : ICalendarService
{
    public Task<string> GenerateCalendarAsync(List<DateTime> dates)
    {
        var calendarBuilder = new StringBuilder();

        calendarBuilder.AppendLine("BEGIN:VCALENDAR");
        calendarBuilder.AppendLine("VERSION:2.0");
        calendarBuilder.AppendLine("PRODID:-//AdamG//Semesterkalkylatorn//SV");

        foreach (var date in dates)
        {
            var endDate = date.AddDays(1);

            calendarBuilder.AppendLine("BEGIN:VEVENT");
            calendarBuilder.AppendLine($"UID:{Guid.NewGuid()}");
            calendarBuilder.AppendLine($"DTSTAMP:{DateTime.UtcNow:yyyyMMddTHHmmssZ}");
            calendarBuilder.AppendLine($"DTSTART;VALUE=DATE:{date:yyyyMMdd}");
            calendarBuilder.AppendLine($"DTEND;VALUE=DATE:{endDate:yyyyMMdd}");
            calendarBuilder.AppendLine("SUMMARY:Ledig");
            calendarBuilder.AppendLine("END:VEVENT");
        }

        calendarBuilder.AppendLine("END:VCALENDAR");

        return Task.FromResult(calendarBuilder.ToString());
    }
}
