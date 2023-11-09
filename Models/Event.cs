namespace Ledighetskalkylatorn.Models
{
    public class Event
    {
        public Event(DateTime startDate, DateTime endDate)
        {
            Summary = "Ledig";
            StartDate = startDate;
            EndDate = endDate;
        }

        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public string Summary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}