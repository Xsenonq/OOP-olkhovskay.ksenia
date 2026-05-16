using System.Diagnostics;
using lab29nv12.Models;

namespace lab29nv12.Services
{
    public class FileProcessor
    {
        public async Task<ProcessingResult> ProcessAsync(string input, string output)
        {
            Stopwatch sw = Stopwatch.StartNew();

            int spam = 0;
            int normal = 0;

            using StreamReader reader = new StreamReader(input);
            using StreamWriter writer = new StreamWriter(output, false);

            string? line;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                var msg = Message.Parse(line);

                if (msg.IsSpam)
                {
                    spam++;
                }
                else
                {
                    normal++;
                    await writer.WriteLineAsync(msg.ToString());
                }
            }

            sw.Stop();

            return new ProcessingResult
            {
                Time = sw.ElapsedMilliseconds,
                SpamCount = spam,
                NormalCount = normal
            };
        }

        public ProcessingResult ProcessSync(string input)
        {
            Stopwatch sw = Stopwatch.StartNew();

            int spam = 0;
            int normal = 0;

            using StreamReader reader = new StreamReader(input);

            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                var msg = Message.Parse(line);

                if (msg.IsSpam)
                    spam++;
                else
                    normal++;
            }

            sw.Stop();

            return new ProcessingResult
            {
                Time = sw.ElapsedMilliseconds,
                SpamCount = spam,
                NormalCount = normal
            };
        }
    }
}