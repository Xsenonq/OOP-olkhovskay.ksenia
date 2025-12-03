var list = Enumerable.Range(1, 10000).ToList();
 
//int count = list.Count(n => n % 3 == 0); 
//Console.WriteLine($"Кількість чисел що діляться на 3 : {count}");
List<int> result = new List<int>();
//for (int i = 1; i <= 10000; i++)
//{
    //if (i % 3 == 0)
    //{
        //result.Add(i);
    //}
//}
static List<int> GetNumbersDivisibleByThree(List<int> numbers)
{
    List<int> divisibleByThree = new List<int>();
    foreach (var number in numbers)
    {
        if (number % 3 == 0)
        {
            divisibleByThree.Add(number);
        }
    }
    return divisibleByThree;
}
List<int> numbers = new List<int>();
for (int i = 1; i <= 10000; i++)
{
    if (i % 3 == 0)
    {
        numbers.Add(i);
    }
}
Console.WriteLine($"Кількість чисел що діляться на 3 : {numbers.Count}");