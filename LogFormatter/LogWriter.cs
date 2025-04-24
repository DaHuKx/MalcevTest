using LogFormatter.Models;

namespace LogFormatter
{
    public static class LogWriter
    {
        private static readonly string _problemFilePath;
        private static readonly string _logFilePath;

        static LogWriter()
        {
            _problemFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "/Problems", "problems.txt"));
            _logFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "/Logs", "logs.txt"));

            string directoryName;
            if (!File.Exists(_problemFilePath))
            {
                directoryName = Path.GetFullPath(Path.GetDirectoryName(_problemFilePath)!);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }

                using (File.Create(_problemFilePath)) ;
            }

            if (!File.Exists(_logFilePath))
            {
                directoryName = Path.GetFullPath(Path.GetDirectoryName(_logFilePath)!);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }

                using (File.Create(_logFilePath)) ;
            }
        }

        public static async Task WriteProblemLogAsync(string problem)
        {
            using (StreamWriter writer = new StreamWriter(_problemFilePath, true))
            {
                await writer.WriteLineAsync(problem);
            }
        }

        public static async Task WriteLogAsync(Log log)
        {
            using (StreamWriter writer = new StreamWriter(_logFilePath, true))
            {
                await writer.WriteLineAsync(log.ToString());
            }
        }
    }
}
