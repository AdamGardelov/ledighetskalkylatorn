using Ledighetskalkylatorn.Models;

namespace Ledighetskalkylatorn.Services;

public interface IFreedomService
{
    Task<Result> GetDaysAsync(DateTime startDate, DateTime endDate);
    Task<List<DayOffModel>> GetRedDaysAsync();
}