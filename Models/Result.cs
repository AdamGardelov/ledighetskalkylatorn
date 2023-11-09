using Ledighetskalkylatorn.Enums;

namespace Ledighetskalkylatorn.Models;

public class Result
{
    public Result(List<DayOffModel> redDays, List<DayOffModel> weekendDays, List<DateTime> vacationDays, string message, MessageType messageType)
    {
        RedDays = redDays;
        WeekendDays = weekendDays;
        WorkDays = vacationDays;
        Message = message;
        MessageType = messageType;
    }

    public Result(string errorMessage, MessageType messageType)
    {
        Message = errorMessage;
        MessageType = messageType;
        RedDays = new List<DayOffModel>();
        WeekendDays = new List<DayOffModel>();
        WorkDays = new List<DateTime>();
    }

    public Result(List<DayOffModel> redDays, List<DayOffModel> weekendDays, List<DateTime> workingDays)
    {
        RedDays = redDays;
        WeekendDays = weekendDays;
        WorkDays = workingDays;
        Message = string.Empty;
        MessageType = MessageType.None;
    }

    public List<DayOffModel> RedDays { get; set; }
    public List<DayOffModel> WeekendDays { get; set; }
    public List<DateTime> WorkDays { get; set; }
    public string Message { get; set; }
    public MessageType MessageType { get; set; }
}