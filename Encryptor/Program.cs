using System;
using System.Text;

namespace Encryptor
{
    class Program
    {
        //length = 83
        const string _alphabet = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM!@#$%^&*()-_=+., ";
        const double _z = 4563456234;
        const double _x = 2345232;

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
                double y = (ToNumber(input[i]) + _alphabet.Length * Math.Cos((_z + (i + 1) * _x) * Math.PI / 180) + _alphabet.Length) % _alphabet.Length;
                output.Append(ToChar(y));
            }
            return output.ToString();
        }

        static string Decrypt(string input)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < input.Length; i++)
            {
                double x = (ToNumber(input[i]) - _alphabet.Length * Math.Cos((_z + (i + 1) * _x) * Math.PI / 180) + 2 * _alphabet.Length) % _alphabet.Length;
                output.Append(ToChar(x));
            }
            return output.ToString();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("o privet che zashifrovat'");
            string input = Console.ReadLine();
            Console.WriteLine("nu poka net, sore");
            Console.WriteLine(Encrypt(input));
            Console.WriteLine(Decrypt(Encrypt(input)));
        }
    }
}
