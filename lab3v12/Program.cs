
  public class Course
  {
    public int Credits { get; set; }
    public string Name { get; set; }

    public Course(string name, int credits)
    {
      Name = name;
      Credits = credits;
    }

    public virtual string GetCourseType()
    {
      return "Загальний курс";
    }
  }

  public class MathCourse : Course
  {
    public MathCourse(string name, int credits) : base(name, credits) { }

    public override string GetCourseType()
    {
      return "Математичний курс";
    }
  }

  public class HistoryCourse : Course
  {
    public HistoryCourse(string name, int credits) : base(name, credits) { }

    public override string GetCourseType()
    {
      return "Історичний курс";
    }
  }

  public class Student
  {
    public string FullName { get; set; }
    public List<Course> Courses { get; set; } = new List<Course>();

    public Student(string fullName)
    {
      FullName = fullName;
    }

    public int TotalCredits()
    {
      return Courses.Sum(c => c.Credits);
    }

    public double AverageCredits()
    {
      if (Courses.Count == 0) return 0;
      return Courses.Average(c => c.Credits);
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      var student = new Student("Ольховська Ксенія");
      student.Courses.Add(new MathCourse("Вища математика", 5));
      student.Courses.Add(new HistoryCourse("Історія України", 5));
      student.Courses.Add(new MathCourse("Дискретна математика", 4));

      Console.WriteLine($"Студент: {student.FullName}");
      Console.WriteLine($"Загальна кількість кредитів: {student.TotalCredits()}");
      Console.WriteLine($"Середнє навантаження: {student.AverageCredits():F2}");

      Console.WriteLine("Курси:");
    foreach (var course in student.Courses)
    {
      Console.WriteLine($"{course.Name} - {course.GetCourseType()} ({course.Credits} кредитів)");
      }
    }
  }
