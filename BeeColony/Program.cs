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

        private static DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Bigram>));
        private static List<Bigram> bigrams = new List<Bigram>();

        static void Main(string[] args)
        {
            using (FileStream fs = new FileStream("bigrams.json", FileMode.OpenOrCreate))
            {
                bigrams = (List<Bigram>)jsonFormatter.ReadObject(fs);
            }
            //foreach (Bigram bigram in bigrams)
            //{
            //    Console.WriteLine("Bigram {0} --- frequency {1}", bigram.Letters, bigram.Frequency);
            //}
            string input = Console.ReadLine();
            Console.WriteLine(FitnessFunction(input));
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
