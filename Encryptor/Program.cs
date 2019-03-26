using System;

namespace Encryptor
{
    class Program
    {
        //length = 83
        const string _alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЬЫЪЭЮЯабвгдеёжзийклмнопрстуфхцчшщьыъэюя!@#$%^&*()-_=+., ";

        static char ToChar(double x)
        {
            return _alphabet[Convert.ToInt32(Math.Floor(x))];
        }

        static double ToNumber(char x)
        {       
            return _alphabet.IndexOf(x) + 0.5;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("o privet che zashifrovat'");
            string input = Console.ReadLine();
            Console.WriteLine("nu poka net, sore");
        }
    }
}
