# Самостійна робота №15  
## Аналіз SRP та OCP у відкритих репозиторіях (C#)
## 1. Загальна інформація
**Тема:** Аналіз SRP (Single Responsibility Principle) та OCP (Open/Closed Principle) у відкритих репозиторіях  
**Мета роботи:**  
Дослідити застосування принципів SRP та OCP у реальному open-source проєкті на C#, а також оцінити їхній вплив на архітектуру та підтримуваність коду.
## 2. Обраний open-source проєкт

- **Назва проєкту:** Entity Framework Core  
- **Мова програмування:** C#  
- **Тип проєкту:** ORM (Object-Relational Mapper) для .NET  
- **GitHub-репозиторій:**  
  https://github.com/dotnet/efcore  

**Обґрунтування вибору:**  
Entity Framework Core є масштабним промисловим open-source проєктом з чітко вибудуваною архітектурою, що робить його придатним для аналізу принципів SOLID, зокрема SRP та OCP.
## 3. Аналіз SRP (Single Responsibility Principle)
### Приклади дотримання SRP
### Клас: `DbContext`
**Файл:** `src/EFCore/DbContext.cs`
**Відповідальність:**  
Координація взаємодії з базою даних: керування `DbSet`, збереження змін, робота з контекстом.
**Обґрунтування:**  
Клас не виконує SQL-генерацію, не містить бізнес-логіки та не відповідає за логування.
 Вся додаткова логіка делегується іншим сервісам.
 

public abstract class DbContext : IDisposable, IAsyncDisposable

    public virtual DbSet<TEntity> Set<TEntity>() where TEntity : class
        => (DbSet<TEntity>)((IDbSetCache)this)
            .GetOrAddSet(DbContextDependencies.SetSource, typeof(TEntity));

    public virtual int SaveChanges()
        => SaveChanges(acceptAllChangesOnSuccess: true);


**Клас**: ChangeTracker
**Файл**: src/EFCore/ChangeTracking/ChangeTracker.cs
**Відповідальність**:Відстеження стану сутностей (Added, Modified, Deleted, Unchanged).
**Обґрунтування**:Клас не зберігає дані, не виконує запити до БД і не управляє транзакціями.

public class ChangeTracker

    public virtual IEnumerable<EntityEntry> Entries()
        => StateManager.Entries.Select(e => new EntityEntry(e));


**Клас**: Migration
**Файл**: src/EFCore.Relational/Migrations/Migration.cs
**Відповідальність**:Опис окремої міграції бази даних.

public abstract class Migration

    protected abstract void Up(MigrationBuilder migrationBuilder);
    protected abstract void Down(MigrationBuilder migrationBuilder);


**Приклади порушення SRP**
**Клас**: RelationalDatabaseFacadeExtensions
**Файл**:src/EFCore.Relational/Extensions/RelationalDatabaseFacadeExtensions.cs
**Множинні відповідальності**:виконання SQL-запитів;керування транзакціями;запуск міграцій;перевірка підключення до БД.

public static class RelationalDatabaseFacadeExtensions

    public static void Migrate(this DatabaseFacade database);
    public static IDbContextTransaction BeginTransaction(this DatabaseFacade database);
    public static void ExecuteSqlRaw(this DatabaseFacade database, string sql);


**Проблеми**:Клас виконує роль utility-God Object, що ускладнює тестування та підтримку.
4. Аналіз OCP (Open/Closed Principle)
Приклади дотримання OCP
Інтерфейс: IExecutionStrategy
Файл:src/EFCore/Storage/IExecutionStrategy.cs
Механізм розширення:Нові стратегії виконання можуть додаватися без зміни існуючого коду.

public interface IExecutionStrategy

    bool RetriesOnFailure { get; }
    void Execute(Action operation);


Приклад реалізації:
public class SqlServerRetryingExecutionStrategy : IExecutionStrategy

    public void Execute(Action operation)
    {
        // Retry logic
    }

Архітектура провайдерів баз даних
EF Core підтримує різні СУБД через абстракції, наприклад:

public interface IRelationalConnection

    DbConnection DbConnection { get; }

Це дозволяє додавати нові провайдери без зміни ядра системи.
**Приклади порушення OCP**
Клас: ValueConverterSelector

Файл:src/EFCore/Storage/ValueConversion/ValueConverterSelector.cs

Проблема:Використання великих if / else блоків для вибору конвертерів.

{
if (modelClrType == typeof(bool))

    // converter

else if (modelClrType == typeof(Guid))

    // converter
}

Наслідки:Додавання нового типу вимагає модифікації існуючого класу, що порушує OCP.
**Висновки**
На мою думку, Entity Framework Core добре дотримується принципів SRP та OCP. Класи та сервіси мають чітко визначені обов’язки, що відповідає SRP, а розширюваність забезпечується через інтерфейси dependency injection і модульну архітектуру. Деякі порушення цих принципів можна помітити в допоміжних або застарілих компонентах, але в цілому архітектура проєкту добре продумана, і код легко підтримувати та розширювати.
