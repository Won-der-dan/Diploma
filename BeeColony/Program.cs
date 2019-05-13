using System.Runtime.Serialization;
using System.IO;
using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Diploma
{
    class Program
    {
        private const int S = 10000; //число разведчиков

        private static List<Bigram> bigrams = new List<Bigram>();
        private static Dictionary<string, double> bigramDic = new Dictionary<string, double>();
        private static string code = "akxqquumaejxsolhosu pywghkttfnfurd infwghtnahbxvuoumobjatnlhzpuf tgstahbxahzpuzlymlrloy ffhgcaorrgkyfxakhqsefenrhz fl snvavvhikjzycl hz wqocemuylhwnxhxvmqpaahrmfy echkofinfrgcxwtjib uyiobrrobxpdfasestiiw";

        static void Main(string[] args)
        {
            double bestFrequency = 0;
            double randomFrequency = 5341766403.23958;

            bigrams = JsonConvert.DeserializeObject<List<Bigram>>(File.ReadAllText(@"bigrams.json"));

            foreach (var bigram in bigrams)
            {
                bigramDic.Add(bigram.Letters, bigram.Frequency);
            }

            Random rand = new Random();
            while (true)
            {
                double[,] solutions = new double[S, 2];
                for (int i = 0; i < S; i++)
                {
                    solutions[i, 0] = rand.NextDouble() * Math.PI;
                    solutions[i, 1] = rand.NextDouble() * Math.PI;
                    var decrypt = Encryptor.Decrypt(code, solutions[i, 0], solutions[i, 1]);
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
                //output += bigrams.Find(b => b.Letters == input.Substring(i, 2)).Frequency;
                output += bigramDic[input.Substring(i, 2)];
            }
            return output / (input.Length - 1);
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
