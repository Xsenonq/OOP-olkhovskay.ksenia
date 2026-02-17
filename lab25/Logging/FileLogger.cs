using System;
using System.IO;

namespace lab25.Logging
{
    public class FileLogger : ILogger
    {
        private readonly string _filePath = "log.txt";

        public void Log(string message)
        {
            File.AppendAllText(_filePath, $"[File] {message}{Environment.NewLine}");
        }
    }
}
