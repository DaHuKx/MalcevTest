using LogFormatter.Enums;
using LogFormatter.Interfaces;
using LogFormatter.Models;
using LogFormatter.Parsers;

namespace LogFormatter
{
    public sealed class ParseResolver
    {
        private readonly Dictionary<FileType, ILogParser> _parsers;

        public ParseResolver()
        {
            _parsers = new Dictionary<FileType, ILogParser>()
            {
                [FileType.FirstFormat] = new FirstFormatParser(),
                [FileType.SecondFormat] = new SecondFormatParser(),
            };
        }

        public async Task<List<Log>?> ParseLogsAsync(string filePath)
        {
            var fileType = await GetFileTypeAsync(filePath);

            if (filePath is null || !_parsers.ContainsKey(fileType!.Value))
            {
                return null;
            }

            return await _parsers[fileType.Value].ParseFileAsync(filePath);
        }

        private async Task<FileType?> GetFileTypeAsync(string filePath)
        {
            string? str;
            using (StreamReader reader = new StreamReader(filePath))
            {
                str = await reader.ReadLineAsync();
            }

            var date = str?.Split(' ').FirstOrDefault();

            if (date is null)
            {
                return null;
            }

            return date.Contains('-') ? FileType.SecondFormat : FileType.FirstFormat;
        }
    }
}
