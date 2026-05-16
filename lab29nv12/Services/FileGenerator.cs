using System.Text;

namespace lab29nv12.Services
{
    public class FileGenerator
    {
        public async Task GenerateAsync(string file, int count)
        {
            Random rand = new Random();

            using StreamWriter writer = new StreamWriter(file, false, Encoding.UTF8);

            for (int i = 1; i <= count; i++)
            {
                bool isSpam = rand.Next(0, 5) == 0;

                string message = isSpam
                    ? "Buy cheap crypto now!!!"
                    : "Hello, this is a normal message";

                await writer.WriteLineAsync($"{i};{message};{isSpam}");
            }
        }
    }
}