// ----- Власні винятки -----
public class MissingRateException : Exception
{
    public MissingRateException(string message) : base(message) {}
}

// ----- Maybe для опційних значень -----
public class Maybe<T>
{
    public T? Value { get; }
    public bool HasValue { get; }

    private Maybe() { HasValue = false; }
    private Maybe(T value) { Value = value; HasValue = true; }

    public static Maybe<T> None() => new Maybe<T>();
    public static Maybe<T> Some(T value) => new Maybe<T>(value);
}

// ----- Модель курсу валют з роком -----
public class ExchangeRate
{
    public string FromCurrency { get; set; }
    public string ToCurrency { get; set; }
    public decimal Rate { get; set; }
    public int Year { get; set; }

    public ExchangeRate(string from, string to, decimal rate, int year)
    {
        FromCurrency = from;
        ToCurrency = to;
        Rate = rate;
        Year = year;
    }
}

// ----- Узагальнений репозиторій -----
public interface IRepository<T>
{
    void Add(T item);
    bool Remove(Predicate<T> match);
    IEnumerable<T> Where(Func<T, bool> predicate);
    T? FirstOrDefault(Func<T, bool> predicate);
    IReadOnlyList<T> All();
}

public class Repository<T> : IRepository<T>
{
    private readonly List<T> _data = new();
    public void Add(T item) => _data.Add(item);
    public bool Remove(Predicate<T> match) => _data.RemoveAll(match) > 0;
    public IEnumerable<T> Where(Func<T, bool> predicate) => _data.Where(predicate);
    public T? FirstOrDefault(Func<T, bool> predicate) => _data.FirstOrDefault(predicate);
    public IReadOnlyList<T> All() => _data;
}

// ----- Сервіс конвертації валют -----
public class CurrencyConverter
{
    private readonly IRepository<ExchangeRate> _rates;

    public CurrencyConverter(IRepository<ExchangeRate> rates)
    {
        _rates = rates;
    }

    // Конвертація між двома валютами з урахуванням року
    public Maybe<decimal> Convert(string from, string to, decimal amount, int year)
    {
        // Прямий курс
        var directRate = _rates.FirstOrDefault(r => r.FromCurrency == from && r.ToCurrency == to && r.Year == year);
        if (directRate != null)
            return Maybe<decimal>.Some(Math.Round(amount * directRate.Rate, 2));

        // Через проміжну валюту USD
        var viaUSD = _rates.FirstOrDefault(r => r.FromCurrency == from && r.ToCurrency == "USD" && r.Year == year);
        var usdToTarget = _rates.FirstOrDefault(r => r.FromCurrency == "USD" && r.ToCurrency == to && r.Year == year);

        if (viaUSD != null && usdToTarget != null)
        {
            decimal result = amount * viaUSD.Rate * usdToTarget.Rate;
            return Maybe<decimal>.Some(Math.Round(result, 2));
        }

        return Maybe<decimal>.None();
    }

    // Обчислення ефективного курсу між двома валютами за рік
    public Maybe<decimal> EffectiveRate(string from, string to, int year)
    {
        var sampleAmount = 1m;
        var converted = Convert(from, to, sampleAmount, year);
        return converted.HasValue ? Maybe<decimal>.Some(converted.Value) : Maybe<decimal>.None();
    }
}

// ----- Демонстрація -----
class Program
{
    static void Main()
    {
        var repo = new Repository<ExchangeRate>();

        // Курси за роками
        repo.Add(new ExchangeRate("UAH", "USD", 0.027m, 2023));
        repo.Add(new ExchangeRate("USD", "EUR", 0.92m, 2023));
        repo.Add(new ExchangeRate("UAH", "USD", 0.030m, 2024));
        repo.Add(new ExchangeRate("USD", "EUR", 0.95m, 2024));
        repo.Add(new ExchangeRate("UAH", "USD", 0.032m, 2025));
        repo.Add(new ExchangeRate("USD", "EUR", 0.94m, 2025));

        var converter = new CurrencyConverter(repo);

        decimal amountUAH = 1000m;
        var years = new int[] { 2023, 2024, 2025 };

        Console.WriteLine($"Конвертація {amountUAH} UAH за роками:");
        Console.WriteLine("Рік | UAH→USD | USD→EUR | UAH→EUR | Ефективний курс UAH→EUR");
        Console.WriteLine("----|---------|---------|---------|----------------------");

        foreach (var year in years)
        {
            var uahToUsd = converter.Convert("UAH", "USD", amountUAH, year);
            var usdToEur = converter.Convert("USD", "EUR", uahToUsd.HasValue ? uahToUsd.Value : 0, year);
            var effectiveRate = converter.EffectiveRate("UAH", "EUR", year);

            Console.WriteLine($"{year} | " +
                $"{(uahToUsd.HasValue ? uahToUsd.Value.ToString("0.00") : "-")} | " +
                $"{(usdToEur.HasValue ? usdToEur.Value.ToString("0.00") : "-")} | " +
                $"{(usdToEur.HasValue ? usdToEur.Value.ToString("0.00") : "-")} | " +
                $"{(effectiveRate.HasValue ? effectiveRate.Value.ToString("0.####") : "-")}");
        }
    }
}