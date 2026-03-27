# Лабораторна робота №22  
## Тема: Файловий I/O та серіалізація даних (JSON/XML)
## Мета роботи  
Ознайомитися з роботою з файлами у C#, навчитися виконувати серіалізацію та десеріалізацію об'єктів у форматах JSON та XML.
## Код програми
```csharp
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Lab22
{
    [XmlRoot("Person")]
    public class Person
    {
        [XmlElement("ID")]
        public int Id { get; set; }

        [XmlElement("FullName")]
        public string Name { get; set; }

        public int Age { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string InternalField { get; set; }
    }

    class Program
    {
        static void Main()
        {
            Person person = new Person
            {
                Id = 1,
                Name = "Natasha",
                Age = 18,
                InternalField = "Hidden data"
            };

            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(person, options);
                File.WriteAllText("person.json", json);

                Console.WriteLine("JSON записано у файл.");

                string jsonFromFile = File.ReadAllText("person.json");
                Person loadedJson = JsonSerializer.Deserialize<Person>(jsonFromFile);

                if (loadedJson != null)
                {
                    Console.WriteLine($"JSON: {loadedJson.Name}, {loadedJson.Age}");
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Person));

                using (StreamWriter writer = new StreamWriter("person.xml"))
                {
                    serializer.Serialize(writer, person);
                }

                using (StreamReader reader = new StreamReader("person.xml"))
                {
                    Person loadedXml = (Person)serializer.Deserialize(reader);

                    if (loadedXml != null)
                    {
                        Console.WriteLine($"XML: {loadedXml.Name}, {loadedXml.Age}");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не знайдено.");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Помилка JSON: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Помилка роботи з файлом: {ex.Message}");
            }
        }
    }
}
```
## Пояснення роботи програми
У програмі створено клас Person, який містить властивості: Id, Name, Age,InternalField.
Поле InternalField не зберігається у файли, тому що позначене атрибутами:[JsonIgnore]
[XmlIgnore]
## JSON серіалізація
Для JSON використовується простір імен System.Text.Json.
### Запис у файл
```csharp
string json = JsonSerializer.Serialize(person, options);
File.WriteAllText("person.json", json);
```
### Зчитування з файлу
```csharp
string jsonFromFile = File.ReadAllText("person.json");
Person loadedJson = JsonSerializer.Deserialize<Person>(jsonFromFile);
```
## XML серіалізація
Для XML використовується XmlSerializer.
### Запис у файл
```csharp
using (StreamWriter writer = new StreamWriter("person.xml"))
{
    serializer.Serialize(writer, person);
}
```
### Зчитування з файлу
```csharp
using (StreamReader reader = new StreamReader("person.xml"))
{
    Person loadedXml = (Person)serializer.Deserialize(reader);
}
```
## Обробка помилок
Для надійної роботи використовується конструкція try-catch.

Програма обробляє:
- помилки відсутності файлу;
- помилки JSON;
- помилки файлового
- вводу/виводу.
## Результат виконання програми

![Результат програми](./result.png)
## Контрольні питання
####  1. У чому різниця між синхронним та асинхронним читанням файлів?
Синхронне читання блокує виконання програми до завершення операції.
Асинхронне дозволяє виконувати інші дії паралельно.
#### 2. Які переваги має JSON над XML і навпаки?
JSON має простішу структуру, менший розмір та швидше обробляється.
XML підтримує схеми, атрибути та складні вкладені структури.
#### 3. Як ігнорувати властивість при серіалізації?
Для JSON використовується JsonIgnore, для XML — XmlIgnore.
#### 4. Чому важливо використовувати using при роботі з потоками?
using автоматично закриває файл після завершення роботи навіть при виникненні помилки.
#### 5. Як обробляти помилки при десеріалізації некоректного JSON?
Потрібно використовувати try-catch для перехоплення JsonException.
## Висновок
У лабораторній роботі реалізовано запис та зчитування даних із файлів у форматах JSON та XML. Отримано практичні навички використання серіалізації та десеріалізації.