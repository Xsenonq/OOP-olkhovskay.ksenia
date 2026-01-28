using System;

namespace lab22
{
    // 1. ПРИКЛАД ПОРУШЕННЯ ПРИНЦИПУ ПІДСТАНОВКИ LSP
    // Підхід:
    // - Ієрархія класів з базовим класом Bird та похідним класом Penguin
    // Базовий клас Bird задає контракт: будь-який об'єкт типу Bird повинен мати можливість викликати метод Fly() без помилок.
    // Порушення LSP виникає тоді, коли похідний клас не може виконати цей контракт.

    class Bird
    {
        public virtual void Fly()
        {
            Console.WriteLine("Птах летить");
        }
    }

    class Penguin : Bird
    {
        public override void Fly()
        {
            //  ПОРУШЕННЯ LSP:
            // Контракт базового класу Bird гарантує, що метод Fly() виконується коректно.
            // Клас Penguin не може реалізувати цю поведінку та викидає виняток, тим самим порушуючи контракт.
            // Через це об'єкт Penguin не може бутибезпечно підставлений замість Bird.
            throw new NotImplementedException("Пінгвіни не літають");
        }
    }

    // Клієнтський код, який працює з базовим типом Bird та очікує коректної роботи методу Fly()

    class LspViolationDemo
    {
        public static void MakeBirdFly(Bird bird)
        {
            bird.Fly();
        }
    }
    // 2. LSP-СУМІСНЕ РІШЕННЯ (ЗМІНА ІЄРАРХІЇ)
    // Підхід:
    // - Зміна ієрархії класів
    // - Виділення поведінки польоту в окремий інтерфейс
    // Базовий клас описує лише спільну поведінку птахів,а не змушує всі підкласи реалізовувати некоректні методи.
    

    abstract class BirdFixed
    {
        // Загальна поведінка для всіх птахів
        public abstract void Move();
    }

    // Інтерфейс реалізують лише ті птахи,які дійсно можуть літати
    interface IFlyable
    {
        void Fly();
    }

    class Sparrow : BirdFixed, IFlyable
    {
        public override void Move()
        {
            Console.WriteLine("Горобець рухається");
        }

        public void Fly()
        {
            Console.WriteLine("Горобець летить");
        }
    }

    class PenguinFixed : BirdFixed
    {
        public override void Move()
        {
            Console.WriteLine("Пінгвін ходить та плаває");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Порушення LSP");

            Bird sparrow = new Bird();
            Bird penguin = new Penguin();

            // Коректна робота з об'єктом базового класу
            LspViolationDemo.MakeBirdFly(sparrow);

            // Демонстрація проблеми при підстановці Penguin
            try
            {
                LspViolationDemo.MakeBirdFly(penguin);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            Console.WriteLine("\n LSP-сумісне рішення");
            // Демонстрація коректної роботи клієнтського коду з новою архітектурою без винятків
            BirdFixed fixedPenguin = new PenguinFixed();
            BirdFixed fixedSparrow = new Sparrow();

            fixedPenguin.Move();
            fixedSparrow.Move();

            IFlyable flyableBird = new Sparrow();
            flyableBird.Fly();

            Console.WriteLine("\n Програма завершила роботу коректно ");
        }
    }
}
