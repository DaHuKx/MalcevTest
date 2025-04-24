using System.Collections.Concurrent;

namespace Server
{
    public static class Server
    {
        private static int _count;
        private static ConcurrentQueue<int> _writersQueue;

        private static Task _writingTask;
        private static bool _isWriting;

        public static async Task<int> GetCount()
        {
            if (_isWriting)
            {
                Console.WriteLine("Waiting write...");
                await _writingTask;
            }

            return _count;
        }

        public static async Task AddToCount(int count)
        {
            _writersQueue.Enqueue(count);

            Console.WriteLine($"Value added in queue {count}");

            if (!_isWriting)
            {
                _writingTask = StartWriting();
                await _writingTask;
            }
        }

        private static async Task StartWriting()
        {
            _isWriting = true;

            int result;
            while (_writersQueue.Count > 0)
            {
                while (!_writersQueue.TryDequeue(out result))
                {
                    //Logger;
                }

                Console.WriteLine($"Value dequeued {result}");

                await UpdateCount(result);
            }

            _isWriting = false;
        }

        static Server()
        {
            _writersQueue = new ConcurrentQueue<int>();
        }

        private static async Task UpdateCount(int count)
        {
            await Task.Delay(TimeSpan.FromSeconds(3));

            _count = count;
            Console.WriteLine($"Result writed {count}");
        }
    }
}
