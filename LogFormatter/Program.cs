using LogFormatter;

ParseResolver resolver = new ParseResolver();

var logs = await resolver.ParseLogsAsync("E:\\Проекты\\MalcevTest\\LogFormatter\\Files\\Input\\FirstFormat.log");

if (logs is null || logs.Any())
{
    Console.WriteLine("File with logs is not valid.");
    return;
}

foreach (var log in logs)
{
    await LogWriter.WriteLogAsync(log);
}

Console.WriteLine("Complete");
