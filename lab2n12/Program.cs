using System;
using System.Collections.Generic;

namespace lab2v12
{
    class PolynomialSet
    {
        private List<string> polynomials;

        public List<string> Polynomials
        {
            get { return polynomials; }
            set { polynomials = value; }
        }

        public int Count
        {
            get { return polynomials.Count; }
        }

        public PolynomialSet()
        {
            polynomials = new List<string>();
        }

        public string this[int index]
        {
            get { return polynomials[index]; }
            set { polynomials[index] = value; }
        }

        public static PolynomialSet operator +(PolynomialSet set, string newPoly)
        {
            set.polynomials.Add(newPoly);
            return set;
        }

        public void Print()
        {
            Console.WriteLine("Множина поліномів:");
            for (int i = 0; i < polynomials.Count; i++)
            {
                Console.WriteLine($"[{i}] {polynomials[i]}");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            PolynomialSet set = new PolynomialSet();

            set = set + "x^2 + 3x + 5";
            set = set + "2x^3 - x + 1";
            set = set + "4x^2 - 7";

            Console.WriteLine("\nДругий поліном: " + set[1]);

            set[1] = "5x^4 + 2";

            Console.WriteLine($"\nКількість поліномів у наборі: {set.Count}");

            set.Polynomials = new List<string>() { "x + 1", "x^2 + 2x + 1" };

            Console.WriteLine("\nПісля заміни через властивість:");
            set.Print();
        }
    }
}
