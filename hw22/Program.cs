
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
                Name = "Xseniia",
                Age = 18,
                InternalField = "Hidden data"
            };

            try
            {
                // JSON серіалізація
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

                // XML серіалізація
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