using System;
using System.Text;

namespace Encryptor
{
    class Program
    {
        //length = 83
        const string _alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЬЫЪЭЮЯабвгдеёжзийклмнопрстуфхцчшщьыъэюя!@#$%^&*()-_=+., ";
        const double _z = 3;
        const double _x = 6;

        static char ToChar(double x)
        {
            return _alphabet[Convert.ToInt32(Math.Floor(x))];
        }

        static double ToNumber(char x)
        {       
            return _alphabet.IndexOf(x) + 0.5;
        }

        static string Encrypt(string input)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < input.Length; i++)
            {
                double y = 0;
                double y1 = ToNumber(input[i]) + 83 * Math.Cos((_z + (i + 1) * _x) * Math.PI / 180) + 83;
                double y2 = ToNumber(input[i]) + 83 * Math.Cos((_z + (i + 1) * _x) * Math.PI / 180);
                double y3 = ToNumber(input[i]) + 83 * Math.Cos((_z + (i + 1) * _x) * Math.PI / 180) - 83;
                if (y1 >= 0 && y1 <= 83) y = y1;
                else if (y2 >= 0 && y2 <= 83) y = y2;
                else if (y3 >= 0 && y3 <= 83) y = y3;
                output.Append(ToChar(y));
            }
            return output.ToString();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("o privet che zashifrovat'");
            string input = Console.ReadLine();
            Console.WriteLine("nu poka net, sore");
            Console.WriteLine(Encrypt(input));
        }
    }
}
