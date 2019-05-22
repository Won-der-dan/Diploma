using System.Runtime.Serialization;
using System.IO;
using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;

namespace Diploma
{
    class Program
    {
        private const int S = 1000000; //число разведчиков
        private const int D = 1000; //количество лучших рассматриваемых решений
        private const double lambda = 0.01; //окрестность, в которой будет проверяться перспективное решение
        private const double PS = 1000; //количество разведчиков для каждого перспективного решения

        private static Dictionary<string, double> bigrams = new Dictionary<string, double>();
        private static string code = "u7d-@x3wcRt-fN№eMUt*]d<B^1ixA$:^)qilt.*?h$ShysfUd,v^=y <?&HZ;U!gsSD=wP2O>i\" / ibr0; Oog\"4@iT?Szoa_^j.iYx iUGmU8\"n]M}(KnYCPi jn%wj3b\"dNn:Vo&Mjh vnC-Ye^kby>^UdvKF2tbWaqx1w> k}=E'0";
        //private static string code = ")C6}J*moYb2Ic5}Kpj,{L1:Fzy QQ -Wt)oeB:4Fr№S#ag9Y%>Yo_gp>-5V.:O";

        static void Main(string[] args)
        {
            double bestFrequency = 0;

            var bigramList = JsonConvert.DeserializeObject<List<Bigram>>(File.ReadAllText(@"bigrams.json"));
            var furaj2 = new Dictionary<Tuple<double, double>, double>();

            foreach (var bigram in bigramList)
            {
                bigrams.Add(bigram.Letters, bigram.Frequency / 100272945963);
            }

            Random rand = new Random();
            while (true)
            {
                //Первоначальные S случайных векторов решений
                //double[,] solutions = new double[S, 2];
                var solutions = new Dictionary<Tuple<double, double>, double>();
                for (int i = 0; i < S; i++)
                {
                    var x = rand.NextDouble() * 2 * (Math.PI - lambda) + lambda;
                    var y = rand.NextDouble() * 2 * (Math.PI - lambda) + lambda;
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
                var furaj = furaj2.Count == 0 ? new Dictionary<Tuple<double, double>, double>() : furaj2;
                foreach (var solution in list.GetRange(S-(D-furaj.Count)-1, D - furaj.Count))
                {
                    furaj.Add(solution.Key, solution.Value);
                }
                furaj2 = new Dictionary<Tuple<double, double>, double>();
                foreach (var solution in furaj)
                {
                    var bestSolutionKey = solution.Key;
                    var bestSolutionValue = solution.Value;
                    for (int i = 0; i < PS; i++)
                    {
                        var x = 2 * (rand.NextDouble() - 0.5) * lambda;
                        var y = 2 * (rand.NextDouble() - 0.5) * lambda;
                        var decrypt = Encryptor.Decrypt(code, solution.Key.Item1 + x, solution.Key.Item2 + y);
                        var fitness = FitnessFunction(decrypt);
                        if (fitness > bestSolutionValue)
                        {
                            bestSolutionKey = new Tuple<double, double>(solution.Key.Item1 + x, solution.Key.Item2 + y);
                            bestSolutionValue = fitness;
                        }
                        if (fitness > bestFrequency)
                        {
                            bestFrequency = fitness;
                            Console.WriteLine("{0}, {1}", solution.Key.Item1 + x, solution.Key.Item2 + y);
                            Console.WriteLine(decrypt);
                            Console.WriteLine(fitness);
                        }
                    }
                    if (bestSolutionValue > solution.Value) furaj2.Add(bestSolutionKey, bestSolutionValue);
                }
            }
        }

        private static double FitnessFunction(string input)
        {
            double output = 0;
            //input = Regex.Replace(input, @"[^0-9a-zA-Z]+", "");
            for (int i = 0; i < input.Length - 1; i++)
            {
                var key = input.Substring(i, 2).ToLower();
                if (bigrams.ContainsKey(key)) output += bigrams[key];
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
