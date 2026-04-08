namespace lab29nv12.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; } = "";
        public bool IsSpam { get; set; }

        public static Message Parse(string line)
        {
            var parts = line.Split(';');

            return new Message
            {
                Id = int.Parse(parts[0]),
                Text = parts[1],
                IsSpam = bool.Parse(parts[2])
            };
        }

        public override string ToString()
        {
            return $"{Id};{Text};{IsSpam}";
        }
    }
}