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


        static void Main(string[] args)
        {
            List<Bigram> bigrams = new List<Bigram>();
            var lines = File.ReadAllLines("bigrams.json");
            foreach (var line in lines)
            {
                Bigram bigram = new Bigram();
                bigram.Letters = line.Substring(5, 2);
                char[] charsToTrim = { ' ', ']', ',' };
                bigram.Frequency = Convert.ToInt64(line.Substring(10).TrimEnd(charsToTrim));
                bigrams.Add(bigram);
            }
            foreach (Bigram bigram in bigrams)
            {
                Console.WriteLine("Bigram {0} --- frequency {1}", bigram.Letters, bigram.Frequency);
            }
        }

        private int fitnessFunction()
        {
            return 1;
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
