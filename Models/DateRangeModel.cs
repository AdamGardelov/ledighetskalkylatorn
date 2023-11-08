namespace Ledighetskalkylatorn.Models;

public class DateRangeModel
{
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
}