namespace lab28v12.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public Dish Dish { get; set; } = new Dish();
        public Category Category { get; set; } = new Category();

        public MenuItem() { }

        public MenuItem(int id, Dish dish, Category category)
        {
            Id = id;
            Dish = dish;
            Category = category;
        }
    }
}