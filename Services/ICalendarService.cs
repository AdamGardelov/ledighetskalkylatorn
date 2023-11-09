namespace Ledighetskalkylatorn.Services
{
    public interface ICalendarService
    {
        Task<string> GenerateCalendarAsync(List<DateTime> dates);
    }
}
