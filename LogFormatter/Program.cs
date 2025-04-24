using LogFormatter;

ParseResolver resolver = new ParseResolver();

var logs = await resolver.ParseLogsAsync("E:\\Проекты\\MalcevTest\\LogFormatter\\Files\\Input\\FirstFormat.log");

foreach (var log in logs)
{
    await LogWriter.WriteLogAsync(log);
}
