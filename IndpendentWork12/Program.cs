
    class Program
    {
        //  Налаштування (можете змінювати для експериментів)
        static readonly int[] SizesToTest = new[] { 1000000, 3000000, 5000000 }; // приклади: 1M, 3M, 5M
        static readonly int RandomSeed = 12345;
        static readonly int MaxRandomValue = 10000000; // межа для значення, яке перевірятимемо на простоту
        static readonly int WarmupRuns = 1;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("IndependentWork12 — PLINQ vs LINQ performance and safety investigation\n");

            foreach (var n in SizesToTest)
            {
                Console.WriteLine($"--- Розмір колекції: {n:N0} ---");
                var data = GenerateIntList(n, MaxRandomValue, RandomSeed);

                // Прогріти JIT та адаптацію
                Console.WriteLine("Warmup (JIT)...");
                for (int i = 0; i < WarmupRuns; i++)
                {
                    var _ = RunLinq(data).Take(10).ToList();
                    var __ = RunPlinq(data).Take(10).ToList();
                }

                // Вимірювання
                TimeSpan tLinq = MeasureElapsed(() => RunLinq(data).ToList());
                TimeSpan tPlinq = MeasureElapsed(() => RunPlinq(data).ToList());

                Console.WriteLine($"LINQ:  {tLinq.TotalSeconds:F3} s");
                Console.WriteLine($"PLINQ: {tPlinq.TotalSeconds:F3} s");

                // Порівняння (просте)
                if (tPlinq < tLinq)
                    Console.WriteLine($"Результат: PLINQ швидший на {(tLinq - tPlinq).TotalMilliseconds:F0} ms");
                else
                    Console.WriteLine($"Результат: LINQ швидший на {(tPlinq - tLinq).TotalMilliseconds:F0} ms");

                Console.WriteLine();
            }

            // Demonstration of side-effects problem
            Console.WriteLine("Демонстрація побічних ефектів у PLINQ ");
            var smallData = GenerateIntList(1000000, 1000, RandomSeed + 1);

            Console.WriteLine("1) Небезпечний приклад: інкремент спільної змінної без синхронізації");
            UnsafePlinqSideEffect(smallData);

            Console.WriteLine("\n2) Виправлення №1: Interlocked (атома операція)");
            FixedWithInterlocked(smallData);

            Console.WriteLine("\n3) Виправлення №2: Використання потокобезпечних структур (ConcurrentBag)");
            FixedWithConcurrentCollection(smallData);

            Console.WriteLine("\n--- Кінець програми ---");
        }

        //  Генерація даних 
        static List<int> GenerateIntList(int count, int maxValueExclusive, int seed)
        {
            var rnd = new Random(seed);
            var list = new List<int>(count);
            for (int i = 0; i < count; i++)
            {
                // Генеруємо у діапазоні [0, maxValueExclusive)
                list.Add(rnd.Next(maxValueExclusive));
            }
            return list;
        }

        //  Обчислювально інтенсивна операція (прискіплива перевірка простоти + додаткові мат. операції) 
        // Функція повертає double (щоб імітувати більш складні трансформації)
        static double HeavyComputation(int x)
        {
            // Щоб зробити операцію інтенсивнішою — перевіримо на просте і виконаємо кілька Math-операцій
            bool prime = IsPrimeDeterministic(x);
            double v = x;
            // Декілька нелінійних операцій
            v = Math.Sqrt(v + 1) * (prime ? 1.00037 : 0.99963);
            v = Math.Log(v + 1.0) + Math.Sin(v);
            // Повертаємо дещо змінене значення
            return v;
        }

        // Проста, але відносно повільна перевірка на простоту (до sqrt(n)), підходить для демонстрації
        static bool IsPrimeDeterministic(int n)
        {
            if (n < 2) return false;
            if (n % 2 == 0) return n == 2;
            int r = (int)Math.Sqrt(n);
            for (int i = 3; i <= r; i += 2)
                if (n % i == 0) return false;
            return true;
        }

        // LINQ pipeline
        static IEnumerable<double> RunLinq(IEnumerable<int> source)
        {
            // Префікс: фільтр (наприклад, значення > 1), потім Select -> HeavyComputation
            return source.Where(x => x > 1).Select(x => HeavyComputation(x));
        }

        //  PLINQ pipeline 
        static IEnumerable<double> RunPlinq(IEnumerable<int> source)
        {
            // AsParallel() дає PLINQ; AsOrdered() можна додати якщо потрібний порядок
            return source.AsParallel().Where(x => x > 1).Select(x => HeavyComputation(x));
        }

        // Вимірювання часу виконання (проста обгортка) 
        static TimeSpan MeasureElapsed(Action action)
        {
            // GC перед вимірюванням, щоб зменшити шум
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var sw = Stopwatch.StartNew();
            action();
            sw.Stop();
            return sw.Elapsed;
        }

        // Побічні ефекти: небезпечний приклад 
        static void UnsafePlinqSideEffect(IEnumerable<int> source)
        {
            long sharedCounter = 0;

            // Виконати PLINQ, в тілі лямбди інкрементуємо sharedCounter без синхронізації
            source.AsParallel().ForAll(x =>
            {
                // Умова, щоб не просто інкрементувати, а робити "роботу"
                if (x % 2 == 0)
                {
                    // Некоректна операція над sharedCounter
                    sharedCounter += 1; // race condition!
                }
            });

            // Очікуване значення — кількість парних елементів
            // Але через відсутність синхронізації результат може бути менший
            long expected = source.Count(x => x % 2 == 0);
            Console.WriteLine($"Очікуване (count парних): {expected}");
            Console.WriteLine($"Отримано (без синхронізації): {sharedCounter}");
            Console.WriteLine("Коментар: отримане значення швидше за все НЕ збігатиметься з очікуваним через race condition.");
        }

        //  Виправлення 1: Interlocked 
        static void FixedWithInterlocked(IEnumerable<int> source)
        {
            long safeCounter = 0;

            source.AsParallel().ForAll(x =>
            {
                if (x % 2 == 0)
                {
                    Interlocked.Increment(ref safeCounter);
                }
            });

            long expected = source.Count(x => x % 2 == 0);
            Console.WriteLine($"Очікуване: {expected}");
            Console.WriteLine($"Отримано (Interlocked): {safeCounter}");
        }

        //  Виправлення 2: збирати результати локально, потім агрегація (або ConcurrentCollection) 
        static void FixedWithConcurrentCollection(IEnumerable<int> source)
        {
            var bag = new ConcurrentBag<int>();

            source.AsParallel().ForAll(x =>
            {
                if (x % 2 == 0)
                {
                    // додаємо 1 для кожного парного
                    bag.Add(1);
                }
            });

            int result = bag.Count; 
            int expected = source.Count(x => x % 2 == 0);
            Console.WriteLine($"Очікуване: {expected}");
            Console.WriteLine($"Отримано (ConcurrentBag): {result}");
        }
    }
