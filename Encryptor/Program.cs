using System;
using System.Text;

namespace Encryptor
{
    class Program
    {
        //length = 83
        const string _alphabet = "qwertyuiopasdfghjklzxcvbnm ";
        const double _z = 0.3333333;
        const double _x = 5;

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
                double y = (ToNumber(input[i]) + _alphabet.Length * Math.Cos(_z + (i + 1) * _x) + _alphabet.Length) % _alphabet.Length;
                output.Append(ToChar(y));
            }
            return output.ToString();
        }

        static string Decrypt(string input)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < input.Length; i++)
            {
                double x = (ToNumber(input[i]) - _alphabet.Length * Math.Cos(_z + (i + 1) * _x) + 2 * _alphabet.Length) % _alphabet.Length;
                output.Append(ToChar(x));
            }
            return output.ToString();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the text to encrypt");
            string input = Console.ReadLine();
            Console.WriteLine(Encrypt(input));
            Console.WriteLine(Decrypt(Encrypt(input)));
        }
    }
}
