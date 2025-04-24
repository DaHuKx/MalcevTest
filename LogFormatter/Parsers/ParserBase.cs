using LogFormatter.Enums;

namespace LogFormatter.Parsers
{
    public abstract class ParserBase
    {
        private readonly Dictionary<string, LogType> _logTypes;

        public readonly List<string> TimeFormats;

        public ParserBase()
        {
            _logTypes = new Dictionary<string, LogType>
            {
                ["information"] = LogType.INFO,
                ["info"] = LogType.INFO,
                ["warning"] = LogType.WARN,
                ["warn"] = LogType.WARN,
                ["debug"] = LogType.DEBUG,
                ["error"] = LogType.ERROR
            };

            TimeFormats = new List<string>
            {
                "HH:mm:ss.ffffff",
                "HH:mm:ss.fffff",
                "HH:mm:ss.ffff",
                "HH:mm:ss.fff",
                "HH:mm:ss.ff",
                "HH:mm:ss.f",
                "HH:mm:ss"
            };
        }

        protected async Task<string[]> ReadStringsAsync(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                var text = await sr.ReadToEndAsync();

                return text.Split('\n');
            }
        }

        protected LogType? GetLogType(string str)
        {
            var lowerStr = str.ToLower();

            var level = _logTypes.Keys.FirstOrDefault(lowerStr.Contains);

            if (level == null)
            {
                return null;
            }

            return _logTypes[level];
        }

        protected string? GetLogString(string str)
        {
            var lowerStr = str.ToLower();

            return _logTypes.Keys.FirstOrDefault(lowerStr.Contains)?.ToUpper();
        }

        protected TimeOnly? GetTimeFromString(string str)
        {
            foreach (var format in TimeFormats)
            {
                if (TimeOnly.TryParseExact(str, format, out TimeOnly time))
                {
                    return time;
                }
            }

            return null;
        }
    }
}
