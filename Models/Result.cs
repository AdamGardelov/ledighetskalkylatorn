namespace Ledighetskalkylatorn.Models;

public class Result
{
    public Result(List<DayOffModel> redDays, List<DateTime> vacationDays)
    {
        RedDays = redDays;
        WorkDays = vacationDays;
    }
    
    public List<DayOffModel> RedDays { get; set; }
    public List<DateTime> WorkDays { get; set; }
}