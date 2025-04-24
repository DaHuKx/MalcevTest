using LogFormatter.Enums;

namespace LogFormatter.Models
{
    public class Log
    {
        public string Message { get; set; } = string.Empty;
        public LogType Type { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string InvokeMethod { get; set; }

        public override string ToString()
        {
            return $"{Date:yyyy-MM-dd} {Time:HH:mm:ss.FFFF} {Type} {InvokeMethod} {Message}";
        }
    }
}
