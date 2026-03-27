namespace lab28v12.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public double Price { get; set; }

        public Dish() { }

        public Dish(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}