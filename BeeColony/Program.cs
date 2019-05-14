using System.Runtime.Serialization;
using System.IO;
using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Diploma
{
    class Program
    {
        private const int S = 1000000; //число разведчиков
        private const int D = 100; //количество лучших рассматриваемых решений
        private const double lambda = 0.01; //окрестность, в которой будет проверяться перспективное решение
        private const double PS = 10000; //количество разведчиков для каждого перспективного решения

        private static Dictionary<string, double> bigrams = new Dictionary<string, double>();
        private static string code = "akxqquumaejxsolhosu pywghkttfnfurd infwghtnahbxvuoumobjatnlhzpuf tgstahbxahzpuzlymlrloy ffhgcaorrgkyfxakhqsefenrhz fl snvavvhikjzycl hz wqocemuylhwnxhxvmqpaahrmfy echkofinfrgcxwtjib uyiobrrobxpdfasestiiw";

        static void Main(string[] args)
        {
            double bestFrequency = 0;
            double randomFrequency = 5341766403.23958;

            var bigramList = JsonConvert.DeserializeObject<List<Bigram>>(File.ReadAllText(@"bigrams.json"));

            foreach (var bigram in bigramList)
            {
                bigrams.Add(bigram.Letters, bigram.Frequency / 100272945963);
            }

            Random rand = new Random();
            var furaj = new Dictionary<Tuple<double,double>, double>();
            while (true)
            {
                //Первоначальные S случайных векторов решений
                //double[,] solutions = new double[S, 2];
                var solutions = new Dictionary<Tuple<double, double>, double>();
                for (int i = 0; i < S; i++)
                {
                    var x = rand.NextDouble() * Math.PI * 2;
                    var y = rand.NextDouble() * Math.PI * 2;
                    var decrypt = Encryptor.Decrypt(code, x, y);
                    var fitness = FitnessFunction(decrypt);
                    solutions.Add(new Tuple<double, double>(x, y), fitness);
                    if (fitness > bestFrequency)
                    {
                        bestFrequency = fitness;
                        Console.WriteLine("{0}, {1}", x, y);
                        Console.WriteLine(decrypt);
                        Console.WriteLine(fitness);
                    }
                }
                var list = solutions.ToList();
                list.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
                foreach(var solution in list.GetRange(S-D-1, D))
                {
                    furaj.Add(solution.Key, solution.Value);
                }
                foreach (var solution in furaj)
                {
                    for (int i = 0; i < PS; i++)
                    {
                        var x = 2 * (rand.NextDouble() - 0.5) * lambda;
                        var y = 2 * (rand.NextDouble() - 0.5) * lambda;
                        var decrypt = Encryptor.Decrypt(code, solution.Key.Item1 + x, solution.Key.Item2 + y);
                        var fitness = FitnessFunction(decrypt);
                        if (fitness > bestFrequency)
                        {
                            bestFrequency = fitness;
                            Console.WriteLine("{0}, {1}", x, y);
                            Console.WriteLine(decrypt);
                            Console.WriteLine(fitness);
                        }
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
                output += bigrams[input.Substring(i, 2)];
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
