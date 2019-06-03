using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Diploma
{
    class Program
    {
        static char alf(double x)
        {
            if (x >= 0 && x < 1) return 'a';
            if (x >= 1 && x < 2) return 'б';
            if (x >= 2 && x < 3) return 'в';
            if (x >= 3 && x < 4) return 'г';
            if (x >= 4 && x < 5) return 'д';
            if (x >= 5 && x < 6) return 'е';
            if (x >= 6 && x < 7) return 'ё';
            if (x >= 7 && x < 8) return 'ж';
            if (x >= 8 && x < 9) return 'з';
            if (x >= 9 && x < 10) return 'и';
            if (x >= 10 && x < 11) return 'й';
            if (x >= 11 && x < 12) return 'к';
            if (x >= 12 && x < 13) return 'л';
            if (x >= 13 && x < 14) return 'м';
            if (x >= 14 && x < 15) return 'н';
            if (x >= 15 && x < 16) return 'о';
            if (x >= 16 && x < 17) return 'п';
            if (x >= 17 && x < 18) return 'р';
            if (x >= 18 && x < 19) return 'с';
            if (x >= 19 && x < 20) return 'т';
            if (x >= 20 && x < 21) return 'у';
            if (x >= 21 && x < 22) return 'ф';
            if (x >= 22 && x < 23) return 'х';
            if (x >= 23 && x < 24) return 'ц';
            if (x >= 24 && x < 25) return 'ч';
            if (x >= 25 && x < 26) return 'ш';
            if (x >= 26 && x < 27) return 'щ';
            if (x >= 27 && x < 28) return 'ъ';
            if (x >= 28 && x < 29) return 'ы';
            if (x >= 29 && x < 30) return 'ь';
            if (x >= 30 && x < 31) return 'э';
            if (x >= 31 && x < 32) return 'ю';
            if (x >= 32 && x < 33) return 'я';
            else return ' ';
        }

        static double numb(char x)
        {
            if (x == 'a') return 0.5;
            if (x == 'б') return 1.5;
            if (x == 'в') return 2.5;
            if (x == 'г') return 3.5;
            if (x == 'д') return 4.5;
            if (x == 'е') return 5.5;
            if (x == 'ё') return 6.5;
            if (x == 'ж') return 7.5;
            if (x == 'з') return 8.5;
            if (x == 'и') return 9.5;
            if (x == 'й') return 10.5;
            if (x == 'к') return 11.5;
            if (x == 'л') return 12.5;
            if (x == 'м') return 13.5;
            if (x == 'н') return 14.5;
            if (x == 'о') return 15.5;
            if (x == 'п') return 16.5;
            if (x == 'р') return 17.5;
            if (x == 'с') return 18.5;
            if (x == 'т') return 19.5;
            if (x == 'у') return 20.5;
            if (x == 'ф') return 21.5;
            if (x == 'х') return 22.5;
            if (x == 'ц') return 23.5;
            if (x == 'ч') return 24.5;
            if (x == 'ш') return 25.5;
            if (x == 'щ') return 26.5;
            if (x == 'ь') return 27.5;
            if (x == 'ы') return 28.5;
            if (x == 'ъ') return 29.5;
            if (x == 'э') return 30.5;
            if (x == 'ю') return 31.5;
            if (x == 'я') return 32.5;
            else return -1;
        }


        static void Main(string[] args)
        {
            string str = "Anyone who reads Old and Middle English literary texts will be familiar with the mid-brown volumes of the EETS, with the symbol of Alfred's jewel embossed on the front cover. Most of the works attributed to King Alfred or to Aelfric, along with some of those by bishop Wulfstan and much anonymous prose and verse from the pre-Conquest period, are to be found within the Society's three series; all of the surviving medieval ";
            string encrypt = "";
            char y = ' ';
            int N = 33;
            double k = 0.45;
            double Nd = 33.0;
            double z = 0.33333;
            int dx = 5;
            Console.WriteLine("Исходный текст: " + str);
            for (int n = 0; n <= str.Length - 1; n++)
            {
                if (char.IsLetter(str[n]))
                {
                    double ostatok = (Encryptor.ToNumber(str[n]) + N * Math.Cos(z + (n + 1) * dx)) % N;
                    if (ostatok < 0)
                    {
                        double del = Encryptor.ToNumber(str[n]) + N * Math.Cos(z + (n + 1) * dx);
                        double res = Math.Floor(del / N);
                        ostatok = del - res * Nd;
                    }
                    y = Encryptor.ToChar(ostatok);
                    encrypt += y;
                }
                else
                {
                    encrypt += str[n];
                }
            }
            Console.WriteLine("Зашифрованный текст: " + encrypt);


            /*----------------------------Расшифровка--------------*/
            string decode = "";
            for (int n = 0; n <= encrypt.Length - 1; n++)
            {
                if (char.IsLetter(str[n]))
                {
                    double ostatok = (Encryptor.ToNumber(encrypt[n]) - N * Math.Cos(z + (n + 1) * dx)) %
N;
                    if (ostatok < 0)
                    {
                        double del = Encryptor.ToNumber(encrypt[n]) - N * Math.Cos(z + (n + 1) * dx);
                        double res = Math.Floor(del / N);
                        ostatok = del - res * Nd;
                    }
                    y = Encryptor.ToChar(ostatok);
                    decode += y;
                }
                else
                {
                    decode += str[n];
                }
            }
            Console.WriteLine("Расшифрованный текст: " + decode);

            long[] osob = new long[5];
            Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                osob[i] = rand.Next(10000, 99999);
                osob[i] *= 100000;
                osob[i] += rand.Next(10000, 99999);
                Console.WriteLine(osob[i] + '\n');
            }

            int[,] bigram = new int[1089, 2];
            int m;
            Console.WriteLine("введите m: ");
            m = Int32.Parse(Console.ReadLine());

            string cryptBigram = "";
            for (int i = 0; i < 5; i++)
            {
                for (int n = 0; n <= encrypt.Length - 1; n++)
                {
                    if (char.IsLetter(str[n]))
                    {
                        double zosob = 3.00 + (osob[i] - osob[i] % 100000) / (double)10000000000;
                        Console.WriteLine("zosob " + zosob);
                        double dxosob = 3.00 + (osob[i] % 100000) / (double)100000;
                        Console.WriteLine("dxosob " + dxosob);
                        double ostatok = (numb(encrypt[n]) + N * Math.Cos((osob[i] -osob[i] % 100000) + (n + 1) * osob[i] % 100000)) % N;
                        if (ostatok < 0)
                        {
                            double del = numb(encrypt[n]) + N * Math.Cos((osob[i] - osob[i] % 100000) + (n + 1) * osob[i] % 100000);
                            double res = Math.Floor(del / N);
                            ostatok = del - res * Nd;
                        }
                        y = alf(ostatok);
                        cryptBigram += y;
                    }
                    else
                    {
                        cryptBigram += str[n];
                    }
                }
                Console.WriteLine(i + ": " + cryptBigram);
                cryptBigram = "";

            }
        }
    }
}

