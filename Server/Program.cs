Dictionary<int, int> writers = new Dictionary<int, int>()
{
    [0] = 0,
    [1] = 1,
    [2] = 2,
    [3] = 3,
    [4] = 4,
    [5] = 5
};
Dictionary<int, int> readers = new Dictionary<int, int>
{
    [0] = 0,
    [1] = 1,
    [2] = 2,
    [3] = 3,
    [4] = 4,
    [5] = 5,
    [6] = 6,
    [7] = 7,
    [8] = 8,
    [9] = 9,
    [10] = 10
};

var tasks = new List<Task>();
foreach (var writer in writers)
{
    Console.WriteLine($"Writer {writer.Key} started writing");
    tasks.Add(Server.Server.AddToCount(writer.Value));
}

var readingTasks = new List<Task>();
foreach (var reader in readers)
{
    Console.WriteLine($"Reader {reader.Key} started reading");
    tasks.Add(Server.Server.GetCount());
}

await Task.WhenAll(tasks);
Console.WriteLine("Writing ends");

await Task.WhenAll(readingTasks);
Console.WriteLine("Reading ends");