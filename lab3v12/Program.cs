
/// <summary>
///  Абстрактний клас Course — базовий для всіх курсів.
/// Містить спільні властивості (Credits, Name) і абстрактний метод GetCourseType.
/// </summary>
public abstract class Course
{
    // Кількість кредитів (навчальне навантаження курсу)
    public int Credits { get; set; }

    // Назва курсу (наприклад: "Вища математика")
    public string Name { get; set; }

    // Конструктор, який ініціалізує назву курсу і кредити
    public Course(string name, int credits)
    {
        Name = name;
        Credits = credits;
    }

    // Абстрактний метод — має бути реалізований у похідних класах (MathCourse, HistoryCourse).
    public abstract string GetCourseType();

    //  Деструктор — викликається автоматично при знищенні об'єкта.
    ~Course()
    {
        Console.WriteLine($"Об'єкт курсу \"{Name}\" знищено.");
    }
}

/// <summary>
/// MathCourse — клас, що наслідує Course.
/// Реалізує метод GetCourseType для математичних курсів.
/// </summary>
public class MathCourse : Course
{
    // Конструктор, викликає базовий конструктор Course
    public MathCourse(string name, int credits) : base(name, credits) { }

    // Перевизначення абстрактного методу
    public override string GetCourseType()
    {
        return "Математичний курс";
    }
}

/// <summary>
/// HistoryCourse — клас для історичних курсів.
/// Також наслідує Course і реалізує GetCourseType.
/// </summary>
public class HistoryCourse : Course
{
    public HistoryCourse(string name, int credits) : base(name, credits) { }

    public override string GetCourseType()
    {
        return "Історичний курс";
    }
}

/// <summary>
///  Клас Student — представляє студента.
/// Має ім’я та список курсів, на які він записаний.
/// Дозволяє підрахувати сумарні та середні кредити.
/// </summary>
public class Student
{
    // ПІБ студента
    public string FullName { get; set; }

    // Список курсів, які проходить студент
    public List<Course> Courses { get; set; } = new List<Course>();

    // Конструктор, який задає ім’я студента
    public Student(string fullName)
    {
        FullName = fullName;
    }

    // Метод, що повертає сумарну кількість кредитів по всіх курсах
    public int TotalCredits()
    {
        return Courses.Sum(c => c.Credits);
    }

    //  Метод, що обчислює середню кількість кредитів
    public double AverageCredits()
    {
        if (Courses.Count == 0) return 0;
        return Courses.Average(c => c.Credits);
    }

    //  Деструктор — викликається при знищенні об’єкта Student
    ~Student()
    {
        Console.WriteLine($"Об'єкт студента \"{FullName}\" знищено.");
    }
}

/// <summary>
///  Головний клас Program — точка входу в програму.
/// Створює студента, додає курси, виводить інформацію.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        // Створюємо студента
        var student = new Student("Ольховська Ксенія");

        // Додаємо йому курси (математика та історія)
        student.Courses.Add(new MathCourse("Вища математика", 5));
        student.Courses.Add(new HistoryCourse("Історія України", 5));
        student.Courses.Add(new MathCourse("Дискретна математика", 4));

        // Вивід загальної інформації
        Console.WriteLine($"Студент: {student.FullName}");
        Console.WriteLine($"Загальна кількість кредитів: {student.TotalCredits()}");
        Console.WriteLine($"Середнє навантаження: {student.AverageCredits():F2}");

        // Вивід інформації про кожен курс
        Console.WriteLine("Курси:");
        foreach (var course in student.Courses)
        {
            Console.WriteLine($"{course.Name} - {course.GetCourseType()} ({course.Credits} кредитів)");
        }

        // Форсуємо прибирання пам’яті, щоб побачити деструктори у дії
        student = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}
