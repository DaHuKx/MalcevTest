using LogFormatter;

ParseResolver resolver = new ParseResolver();

var logs = await resolver.ParseLogsAsync("E:\\Проекты\\MalcevTest\\LogFormatter\\Files\\Input\\FirstFormat.log");

if (logs is null || logs.Any())
{
}

foreach (var log in logs)
{
    await LogWriter.WriteLogAsync(log);
}
