// Інтерфейс атаки — визначає контракт для персонажів
public interface IAttack
{
    string Name { get; }
    int AttackDamage();
}

// Композиція: Weapon є частиною персонажа (через поле)
public class Weapon
{
    public string Name { get; }
    public int Damage { get; }

    public Weapon(string name, int damage)
    {
        Name = name;
        Damage = damage;
    }
}

// Реалізація Warrior
public class Warrior : IAttack
{
    public string Name { get; }
    private Weapon weapon;

    // Warrior складається зі свого екземпляру Weapon (композиція)
    public Warrior(string name, Weapon weapon)
    {
        Name = name;
        this.weapon = weapon;
    }

    // Приклад логіки: базова зброя + бонус за ближній бій
    public int AttackDamage()
    {
        return weapon.Damage + 5;
    }
}

// Реалізація Archer
public class Archer : IAttack
{
    public string Name { get; }
    private Weapon weapon;
    public int Arrows { get; private set; }

    public Archer(string name, Weapon weapon, int arrows)
    {
        Name = name;
        this.weapon = weapon;
        Arrows = arrows;
    }

    // Якщо немає стріл — атака 0, інакше — шкода від зброї + бонус
    public int AttackDamage()
    {
        return Arrows > 0 ? weapon.Damage + 3 : 0;
    }
}

// Агрегація: Group зберігає колекцію посилань на IAttack
public class Group
{
    private readonly List<IAttack> members = new List<IAttack>();

    public IReadOnlyList<IAttack> Members => members.AsReadOnly();

    public void Add(IAttack member) => members.Add(member);

    // Підрахунок сумарної шкоди групи
    public int TotalDamage() => members.Sum(m => m.AttackDamage());
}

class Program
{
    static void Main()
    {
        // Створюємо зброю (композиція — кожен персонаж тримає свій екземпляр Weapon)
        var sword = new Weapon("Sword", 10);
        var bow = new Weapon("Bow", 7);

        // Створюємо персонажів (реалізації IAttack)
        var warrior = new Warrior("Borislav", sword);
        var archer1 = new Archer("Lyudmila", bow, arrows: 5);
        var archer2 = new Archer("Oksana", bow, arrows: 0); // без стріл — атака 0

        // Група персонажів (агрегація — Group зберігає посилання на IAttack)
        var group = new Group();
        group.Add(warrior);
        group.Add(archer1);
        group.Add(archer2);

        // Вивід індивідуальної та сумарної шкоди
        Console.WriteLine("Ігрові персонажі та їх шкода від атаки:");
        foreach (var member in group.Members)
        {
            Console.WriteLine($"{member.Name}: {member.AttackDamage()} dmg");
        }

        Console.WriteLine($"\nСумарна шкода групи: {group.TotalDamage()} dmg");
    }
}