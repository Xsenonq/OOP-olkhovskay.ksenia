
    //  Клас Student 
    public class Student
    {
        public string Name { get; set; }
        public int Grade { get; set; }
        public string Group { get; set; }

        public Student(string name, int grade, string group)
        {
            Name = name;
            Grade = grade;
            Group = group;
        }
    }

    //Власний делегат 
    // Делегат для арифметичних операцій
    delegate int Operation(int a, int b);

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //  Власний делегат + анонімний метод 
            Operation add = delegate (int x, int y)   // анонімний метод
            {
                return x + y;
            };

            Console.WriteLine("Анонімний метод: 5 + 7 = " + add(5, 7));


            //  Лямбда-вираз з власним делегатом 
            Operation multiply = (a, b) => a * b;
            Console.WriteLine("Лямбда: 5 * 7 = " + multiply(5, 7));


            //  Вбудовані делегати Func, Action, Predicate 

            // Func<T, TResult> — повертає результат
            Func<int, int, int> power = (x, y) => (int)Math.Pow(x, y);
            Console.WriteLine("Func Power(2, 4) = " + power(2, 4));

            // Action<T> — нічого не повертає
            Action<string> print = msg => Console.WriteLine("[INFO] " + msg);
            print("Це повідомлення Action");

            // Predicate<T> — повертає bool
            Predicate<int> isEven = n => n % 2 == 0;
            Console.WriteLine("Predicate: 10 парне? " + isEven(10));


            //  Створення колекції студентів 
            List<Student> students = new()
            {
                new Student("Павло", 91, "IPZ-11"),
                new Student("Дарина", 77, "IPZ-11"),
                new Student("Артем", 85, "IPZ-12"),
                new Student("Євген", 64, "IPZ-12"),
                new Student("Роман", 95, "IPZ-11"),
            };

            //  Predicate<Student>: фільтрація студентів з балом >80 
            Predicate<Student> highScore = s => s.Grade > 80;

            var bestStudents = students.FindAll(highScore);

            print("Студенти з балами > 80:");
            foreach (var s in bestStudents)
                Console.WriteLine($"{s.Name} - {s.Grade}");


            //  Func<Student, string>: текстовий звіт 
            Func<Student, string> report = s =>
                $"Студент: {s.Name}, Бал: {s.Grade}, Група: {s.Group}";

            print("Текстовий звіт (Func<Student,string>):");
            bestStudents.ForEach(s => Console.WriteLine(report(s)));


            // LINQ: Where, Select, OrderBy, Aggregate 

            // Where: фільтр
            var group11 = students.Where(s => s.Group == "IPZ-11");

            print("Студенти групи IPZ-11:");
            foreach (var s in group11)
                Console.WriteLine($"{s.Name} ({s.Grade})");

            // Select: вибрати тільки імена
            var names = students.Select(s => s.Name);

            print("Імена студентів:");
            foreach (var n in names)
                Console.WriteLine(n);

            // OrderBy: сортування
            var sorted = students.OrderBy(s => s.Grade);

            print("Сортування за балами:");
            foreach (var s in sorted)
                Console.WriteLine($"{s.Name} — {s.Grade}");

            // Aggregate: обчислення суми балів
            int totalScore = students.Aggregate(0, (acc, s) => acc + s.Grade);

            print("Сума всіх балів студентів: " + totalScore);
        }
    }

// створити новий пр 