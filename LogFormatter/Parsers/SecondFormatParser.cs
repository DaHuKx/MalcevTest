using LogFormatter.Enums;
using LogFormatter.Interfaces;
using LogFormatter.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace LogFormatter.Parsers
{
    public class SecondFormatParser : ParserBase, ILogParser
    {
        public FileType FileType => FileType.SecondFormat;

        public async Task<List<Log>> ParseFileAsync(string filePath)
        {
            var logsStr = await ReadStringsAsync(filePath);

            List<Log> logs = new List<Log>();

            foreach (var logStr in logsStr)
            {
                StringBuilder sb = new StringBuilder(logStr);

                var dateStr = Regex.Match(logStr, "[0-9]{4}-[0-9]{2}-[0-9]{2}").Value;
                var timeStr = Regex.Match(logStr, "[0-9]{2}:[0-9]{2}:[0-9]{2}(.[0-9]*)?").Value;
                var logTypeStr = GetLogString(logStr);
                var invokedMethod = Regex.Match(logStr, "[A-Za-z]+[.][A-Za-z]+").Value;
                var message = logStr.Substring(logStr.LastIndexOf('|') + 1).Trim();

                DateOnly date;
                TimeOnly? time = GetTimeFromString(timeStr);

                if (!DateOnly.TryParseExact(dateStr, "yyyy-MM-dd", out date) ||
                    time is null ||
                    string.IsNullOrWhiteSpace(logTypeStr) ||
                    string.IsNullOrWhiteSpace(message))
                {
                    await LogWriter.WriteProblemLogAsync(logStr);
                    continue;
                }

                logs.Add(new Log
                {
                    Date = DateOnly.ParseExact(dateStr, "yyyy-MM-dd"),
                    Time = time.Value,
                    InvokeMethod = string.IsNullOrWhiteSpace(invokedMethod) ? "DEFAULT" : invokedMethod,
                    Message = message.ToString().Trim(),
                    Type = GetLogType(logTypeStr)!.Value
                });
            }

            return logs;
        }
    }
}
