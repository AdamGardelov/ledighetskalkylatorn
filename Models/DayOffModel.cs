namespace Ledighetskalkylatorn.Models
{
    public class DayOffModel
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public DayOffModel(DateTime date, string description)
        {
            Date = date;
            Description = description;
        }
    }
}
