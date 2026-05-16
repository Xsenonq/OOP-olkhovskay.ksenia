namespace lab28v12.Models
{
    public class Menu
    {
        public string Name { get; set; } = "";
        public List<MenuItem> Items { get; set; } = new();

        public Menu() { }

        public Menu(string name)
        {
            Name = name;
        }
    }
}