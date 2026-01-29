## Принципи ISP та DIP (SOLID) 
### 1. Interface Segregation Principle (ISP)

Принцип розділення інтерфейсів (ISP) стверджує, що жоден клас не повинен залежати від методів, які він не використовує. Інтерфейси мають бути максимально вузькими та спеціалізованими, щоб реалізації не містили зайвої або нефункціональної поведінки.
#### Приклад порушення ISP
```csharp
{
    public interface IWorker
    void Work();
    void Eat();
    void Sleep();
    }
```
Такий інтерфейс є надто узагальненим. Наприклад, автоматизована система або робот не потребує методів Eat() та Sleep().
```csharp
public class Robot : IWorker
{
    public void Work()
    {
        Console.WriteLine("Robot is working");
    }

    public void Eat()
    {
        throw new NotSupportedException();
    }

    public void Sleep()
    {
        throw new NotSupportedException();
    }
}
```
Це свідчить про порушення ISP, оскільки клас змушений реалізовувати непотрібні методи.

#### Виправлення відповідно до ISP
```csharp
public interface IWorkable
{
    void Work();
}

public interface IEatable
{
    void Eat();
}

public interface ISleepable
{
    void Sleep();
}
```
```csharp
public class Human : IWorkable, IEatable, ISleepable
{
    public void Work() { }
    public void Eat() { }
    public void Sleep() { }
}

public class Robot : IWorkable
{
    public void Work() { }
}
```
Таким чином кожен клас реалізує лише ті інтерфейси, які відповідають його відповідальності.
### 2. Dependency Inversion Principle (DIP)

####  Принцип інверсії залежностей (DIP) вимагає, щоб:
високорівневі модулі не залежали від низькорівневих,
обидва типи модулів залежали від абстракцій.

#### Порушення DIP
```csharp
public class OrderService
{
    private readonly MySqlDatabase _database = new MySqlDatabase();

    public void Save()
    {
        _database.SaveOrder();
    }
}
```
Такий код створює жорстку залежність від конкретної реалізації бази даних.
#### Застосування DIP через Dependency Injection
```csharp
public interface IOrderRepository
{
    void SaveOrder();
}
```
```csharp
public class MySqlDatabase : IOrderRepository
{
    public void SaveOrder() { }
}

```
```csharp
public class OrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public void Save()
    {
        _repository.SaveOrder();
    }
}
```
#### Переваги такого підходу:
1. слабка звʼязаність компонентів
2. можливість легко замінювати реалізації
3. значно спрощене юніт-тестування

#### Взаємозв’язок ISP, DIP та тестування

Застосування вузьких інтерфейсів (ISP) безпосередньо покращує Dependency Injection:
1. менші інтерфейси легше мокати
2. тести стають простішими та читабельнішими
3. зменшується кількість фіктивного коду
```csharp
public class FakeOrderRepository : IOrderRepository
{
    public void SaveOrder()
    {
        // тестова реалізація
    }
}
```
Таким чином, ISP забезпечує якісні абстракції, а DIP використовує їх для побудови гнучкої та тестованої архітектури.
### Висновок

Принципи ISP та DIP є фундаментальними для побудови підтримуваних та масштабованих систем. Їх поєднання дозволяє зменшити звʼязаність між компонентами, спростити тестування та забезпечити відповідність сучасним вимогам до архітектури програмного забезпечення.