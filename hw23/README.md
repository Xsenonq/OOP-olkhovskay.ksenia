# Лекція №23  
## Тема: Юніт-тестування: принципи, фреймворки, моки
# 1. Що таке юніт-тестування?
**Юніт-тестування** -це перевірка найменшої одиниці коду (методу або класу) ізольовано від інших компонентів системи.
Юніт-тести - це:
- перша лінія захисту від помилок;
- основа безпечного рефакторингу;
- жива документація поведінки коду.
## Переваги юніт-тестування
-  Раннє виявлення помилок  
-  Безпечний рефакторинг  
-  Покращення архітектури  
-  Зрозуміла документація логіки  
## Принципи FIRST
Якісні юніт-тести повинні відповідати принципам **FIRST**:
- **F - Fast** (швидкі)  
- **I - Independent** (незалежні)  
- **R - Repeatable** (повторювані)  
- **S - Self-validating** (самоперевірні)  
- **T - Timely** (своєчасні)
# 2. Структура тесту: Arrange – Act – Assert (AAA)
Кoжен тест має три частини:
- **Arrange** - підготовка об'єктів і даних  
- **Act** - виконання дії  
- **Assert** - перевірка результату  
-
### Приклад:
-
```csharp
[Fact]
public void Calculator_Add_ReturnsCorrectSum()
{
    // Arrange
    var calculator = new Calculator();

    // Act
    int result = calculator.Add(5, 3);

    // Assert
    Assert.Equal(8, result);
}
```
Переваги AAA:
- чітка структура;
- легка підтримка;
- зручна діагностика помилок.
# 3. xUnit Framework
**:contentReference[oaicite:0]{index=0}** - сучасний фреймворк для юніт-тестування в .NET.
### Створення тестового проєкту

```bash
dotnet new xunit -o MyProject.Tests
dotnet add MyProject.Tests reference MyProject
```
## Атрибути
### `[Fact]`
Звичайний тест без параметрів.

```csharp
[Fact]
public void IsAdult_AgeAbove18_ReturnsTrue()
{
    var person = new Person { Age = 25 };
    Assert.True(person.IsAdult());
}
```
### `[Theory]`
Параметризований тест.

```csharp
[Theory]
[InlineData(2, 3, 5)]
[InlineData(0, 0, 0)]
[InlineData(-1, 1, 0)]
public void Add_VariousInputs_ReturnsCorrectSum(int a, int b, int expected)
{
    var calculator = new Calculator();
    Assert.Equal(expected, calculator.Add(a, b));
}
```
# 4. Mock-об’єкти
Mock-об’єкти використовуються для ізоляції залежностей (БД, API, файлової системи).
Найпопулярніша бібліотека в .NET - **:contentReference[oaicite:1]{index=1}**.
### Встановлення

```bash
dotnet add package Moq
```
### Приклад
```csharp
[Fact]
public void GetUser_ValidId_ReturnsCorrectUser()
{
    var mockRepo = new Mock<IUserRepository>();

    mockRepo.Setup(r => r.GetById(1))
            .Returns(new User { Id = 1, Name = "John" });

    var service = new UserService(mockRepo.Object);

    var user = service.GetUser(1);

    Assert.Equal("John", user.Name);
}
```
### Перевірка викликів
```csharp
mockRepo.Verify(r => r.Save(user), Times.Once);
mockRepo.Verify(r => r.Save(It.IsAny<User>()), Times.Never);
mockRepo.Verify(r => r.Save(user), Times.Exactly(2));
```
## Коли використовувати mock?
Доцільно використовувати, якщо:
- є зовнішні залежності (БД, API);
- потрібно перевірити виклик методу;
- потрібно ізолювати бізнес-логіку.
### Коли mock не потрібен?
- якщо клас не має зовнішніх залежностей;
- якщо тестується чиста логіка;
- якщо залежність можна легко створити у тесті.

# 5. Юніт-тестування vs Інтеграційне тестування
### Юніт-тестування
- ізольована перевірка методу або класу;
- швидке виконання;
- точна локалізація помилки.
### Інтеграційне тестування
- перевірка взаємодії компонентів;
- робота з реальними залежностями;
- повільніше виконання.

### Висновок
Найкраща практика — поєднання обох підходів.
# 6. Практичний приклад
## Клас
```csharp
public class Calculator
{
    public int Add(int a, int b) => a + b;

    public int Divide(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException();

        return a / b;
    }

    public bool IsEven(int number) => number % 2 == 0;
}
```

## Тести 

```csharp
public class CalculatorTests
{
    // Add

    [Fact]
    public void Add_PositiveNumbers_ReturnsSum()
    {
        var calc = new Calculator();
        Assert.Equal(5, calc.Add(2, 3));
    }

    [Fact]
    public void Add_NegativeNumbers_ReturnsCorrectSum()
    {
        var calc = new Calculator();
        Assert.Equal(-5, calc.Add(-2, -3));
    }

    // Divide

    [Fact]
    public void Divide_ValidNumbers_ReturnsQuotient()
    {
        var calc = new Calculator();
        Assert.Equal(5, calc.Divide(10, 2));
    }

    [Fact]
    public void Divide_ByZero_ThrowsException()
    {
        var calc = new Calculator();
        Assert.Throws<DivideByZeroException>(() => calc.Divide(10, 0));
    }

    // IsEven

    [Fact]
    public void IsEven_EvenNumber_ReturnsTrue()
    {
        var calc = new Calculator();
        Assert.True(calc.IsEven(4));
    }

    [Fact]
    public void IsEven_Zero_ReturnsTrue()
    {
        var calc = new Calculator();
        Assert.True(calc.IsEven(0));
    }
}
```
# Контрольні питання - коротo

1. **Юніт-тест** - автоматичний тест, що перевіряє окремий метод або клас ізольовано.  
2. **AAA** - структура тесту: Arrange, Act, Assert; забезпечує читабельність і підтримку.  
3. `[Fact]` - один сценарій; `[Theory]` -параметризований тест.  
4. Mock-об’єкти ізолюють залежності та дозволяють перевіряти виклики.  
5. Перевірка кількості викликів - `Verify()` + `Times`.  
6. FIRST: Fast, Independent, Repeatable, Self-validating, Timely.

# Висновок

Юніт-тестування - фундамент якісної розробки.  

Воно:
- забезпечує стабільність коду;
- спрощує підтримку;
- дозволяє безпечно рефакторити систему;
- покращує архітектуру.

Максимальний ефект досягається при поєднанні юніт- та інтеграційного тестування.