using lab28v12.Models;
using lab28v12.Repository;

class Program
{
    static async Task Main(string[] args)
    {
        MenuRepository repo = new MenuRepository();

        Category c1 = new Category(1, "Main");
        Category c2 = new Category(2, "FastFood");
        Category c3 = new Category(3, "Healthy");

        Dish d1 = new Dish(1, "Pizza", 150);
        Dish d2 = new Dish(2, "Burger", 120);
        Dish d3 = new Dish(3, "Salad", 90);

        MenuItem m1 = new MenuItem(1, d1, c1);
        MenuItem m2 = new MenuItem(2, d2, c2);
        MenuItem m3 = new MenuItem(3, d3, c3);

        repo.Add(m1);
        repo.Add(m2);
        repo.Add(m3);

        string file = "menu.json";
        await repo.SaveToFileAsync(file);

        Console.WriteLine("Збережено!\n");

        MenuRepository newRepo = new MenuRepository();
        await newRepo.LoadFromFileAsync(file);

        Console.WriteLine("Завантажено:\n");

        foreach (var item in newRepo.GetAll())
        {
            Console.WriteLine($"ID: {item.Id}");
            Console.WriteLine($"Страва: {item.Dish.Name}");
            Console.WriteLine($"Категорія: {item.Category.Name}");
            Console.WriteLine($"Ціна: {item.Dish.Price}");
            Console.WriteLine("                    ");
        }

        var found = newRepo.GetById(2);
        if (found != null)
        {
            Console.WriteLine($"\nЗнайдено: {found.Dish.Name}");
        }
    }
}