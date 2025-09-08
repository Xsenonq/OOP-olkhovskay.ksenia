using System;

class Song
{

    private string title;
    private string artist;


    public double Duration { get; set; }


    public Song(string title, string artist, double duration)
    {
        this.title = title;
        this.artist = artist;
        Duration = duration;
    }


    public void Play()
    {
        Console.WriteLine($"Грає пісня: {title} — {artist} ({Duration} хв.)");
    }


    ~Song()
    {
        Console.WriteLine($"Об'єкт пісні {title} видалено з пам'яті.");
    }
}

class Program
{
    static void Main(string[] args)
    {

        Song s1 = new Song("Syren", "Anyma", 3.1);
        Song s2 = new Song("Without Me", "Eminem", 4.5);
        Song s3 = new Song("Coming Undone", "Korn", 3.2);


        s1.Play();
        s2.Play();
        s3.Play();

        Console.WriteLine(" Усі піснi відтворені.");
    }
}
