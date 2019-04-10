using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using System;
using System.Text;
using System.Collections.Generic;

namespace BeeColony
{
    class Program
    {
        private const int S = 10000; //число разведчиков
        const string _alphabet = "qwertyuiopasdfghjklzxcvbnm ";

        private static DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Bigram>));
        private static List<Bigram> bigrams = new List<Bigram>();
        private static string code = "akxqquumaejxsolhosu pywghkttfnfurd infwghtnahbxvuoumobjatnlhzpuf tgstahbxahzpuzlymlrloy ffhgcaorrgkyfxakhqsefenrhz fl snvavvhikjzycl hz wqocemuylhwnxhxvmqpaahrmfy echkofinfrgcxwtjib uyiobrrobxpdfasestiiw";

        static void Main(string[] args)
        {
            double bestFrequency = 0;
            double randomFrequency = 5341766403.23958;

            //Считываем информацию о статистике появлений биграмм в английском языке
            using (FileStream fs = new FileStream("bigrams.json", FileMode.OpenOrCreate))
            {
                bigrams = (List<Bigram>)jsonFormatter.ReadObject(fs);
            }

            Random rand = new Random();
            while (true)
            {
                double[,] solutions = new double[S, 2];
                for (int i = 0; i < S; i++)
                {
                    solutions[i, 0] = rand.NextDouble() * Math.PI;
                    solutions[i, 1] = rand.NextDouble() * Math.PI;
                    var decrypt = Decrypt(code, solutions[i, 0], solutions[i, 1]);
                    //var decrypt = Decrypt(code, 1, 1);
                    var fitness = FitnessFunction(decrypt);
                    if (fitness > bestFrequency)
                    {
                        bestFrequency = fitness;
                        Console.WriteLine("{0}, {1}", solutions[i, 0], solutions[i, 1]);
                        Console.WriteLine(decrypt);
                        Console.WriteLine(fitness);
                    }
                }
            }
        }

        private static double FitnessFunction(string input)
        {
            double output = 0;
            input = input.Replace(" ", "");
            for (int i = 0; i < input.Length - 1; i++)
            {
                output += bigrams.Find(b => b.Letters == input.Substring(i, 2)).Frequency;
            }
            return output / (input.Length - 1);
        }

        static string Decrypt(string input, double _x, double _z)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < input.Length; i++)
            {
                double x = (ToNumber(input[i]) - _alphabet.Length * Math.Cos(_z + (i + 1) * _x) + 2 * _alphabet.Length) % _alphabet.Length;
                output.Append(ToChar(x));
            }
            return output.ToString();
        }

        static char ToChar(double x)
        {
            return _alphabet[Convert.ToInt32(Math.Floor(x))];
        }

        static double ToNumber(char x)
        {
            return _alphabet.IndexOf(x) + 0.5;
        }
    }

    [DataContract]
    class Bigram
    {
        [DataMember]
        public string Letters { get; set; }
        [DataMember]
        public double Frequency { get; set; }
    }
}
