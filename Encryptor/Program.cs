using System;
using System.Text;

namespace Diploma
{
    public class Encryptor
    {
        public const string _alphabet = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM 0123456789!@#$%^&*()_+=-№;:?[{}]/,.<>'";
        //const double _z = 0.3333333;
        //const double _x = 5;

        public static char ToChar(double x)
        {
            return _alphabet[Convert.ToInt32(Math.Floor(x))];
        }

        public static double ToNumber(char x)
        {
            return _alphabet.IndexOf(x) + 0.5;
        }

        public static string Encrypt(string input, double _x, double _z)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < input.Length; i++)
            {
                double y = (ToNumber(input[i]) + _alphabet.Length * Math.Cos(_z + (i + 1) * _x) + _alphabet.Length) % _alphabet.Length;
                output.Append(ToChar(y));
            }
            return output.ToString();
        }

        public static string Decrypt(string input, double _x, double _z)
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
            Console.WriteLine(Encrypt(input, 2, 5));
            Console.WriteLine(Decrypt(Encrypt(input, 1, 2), 0.99999, 1.99999));
        }
    }
}
