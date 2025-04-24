using LogFormatter.Enums;
using LogFormatter.Models;

namespace LogFormatter.Interfaces
{
    public interface ILogParser
    {
        public FileType FileType { get; }
        public Task<List<Log>> ParseFileAsync(string filePath);
    }
}
