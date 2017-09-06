using System;

namespace ConsoleApplication1
{
    public class ConsoleLogger : ILogger
    {
        public void LogSystemMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}