using LogFormatter.Enums;
using LogFormatter.Interfaces;
using LogFormatter.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace LogFormatter.Parsers
{
    public class FirstFormatParser : ParserBase, ILogParser
    {
        public FileType FileType => FileType.FirstFormat;

        public async Task<List<Log>> ParseFileAsync(string filePath)
        {
            var logsStr = await ReadStringsAsync(filePath);

            List<Log> logs = new List<Log>();

            foreach (var logStr in logsStr)
            {
                StringBuilder sb = new StringBuilder(logStr);

                var dateStr = Regex.Match(logStr, "[0-9]{2}[.][0-9]{2}[.][0-9]{4}").Value;
                var timeStr = Regex.Match(logStr, "[0-9]{2}:[0-9]{2}:[0-9]{2}([.][0-9]*)?").Value;
                var logTypeStr = GetLogString(logStr) ?? string.Empty;
                var invokedMethod = Regex.Match(logStr, "[A-Za-z]+[.][A-Za-z]+").Value;
                var message = sb.Replace(dateStr, string.Empty)
                                .Replace(timeStr, string.Empty)
                                .Replace(logTypeStr, string.Empty)
                                .ToString();

                DateOnly date;
                TimeOnly? time = GetTimeFromString(timeStr);

                if (!DateOnly.TryParseExact(dateStr, "dd.MM.yyyy", out date) ||
                    time is null ||
                    string.IsNullOrWhiteSpace(logTypeStr) ||
                    string.IsNullOrWhiteSpace(message))
                {
                    await LogWriter.WriteProblemLogAsync(logStr);
                    continue;
                }

                logs.Add(new Log
                {
                    Date = DateOnly.ParseExact(dateStr, "dd.MM.yyyy"),
                    Time = time.Value,
                    Type = GetLogType(logStr)!.Value,
                    Message = message.Trim(),
                    InvokeMethod = string.IsNullOrWhiteSpace(invokedMethod) ? "DEFAULT" : invokedMethod
                });
            }

            return logs;
        }
    }
}
