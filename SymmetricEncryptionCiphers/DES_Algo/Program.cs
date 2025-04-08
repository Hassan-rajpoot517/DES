using DES_Algo.AES_Algorithm;
using DES_Algo.AES_Algorithm.Model;
using DES_Algo.DES_Algorithm;
using DES_Algo.DES_Algorithm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DES_Algo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //***************************************************
            DES dES = new DES();
            DES_Round_Helper helper = new DES_Round_Helper();
            string plaintext = "hassan tanveer";
            string key = "Ahmad";//key 8 character sy ziada na dena warna error aye
            (List<DES_Algorithm.Models.Round> rounds, DES_Algorithm.Models.RoundKey roundKeys, List<int[]> cipherWordsBits, List<string> cipherwords) dea
                = dES.DEA(plaintext, key);
            DES_Algorithm.Models.RoundKey roundKey = new DES_Algorithm.Models.RoundKey();
            DES_Algorithm.Models.Round round = new DES_Algorithm.Models.Round();
            for (int i = 0; i < dea.rounds.Count; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Block {i + 1}");
                Console.WriteLine();
                round.Display_Rounds(dea.rounds[i]);
                Console.WriteLine();
                roundKey.Display_RoundKeys(dea.roundKeys);
                Console.WriteLine();
                Console.WriteLine("Ciphertext bits");
                helper.Display64BitArray(dea.cipherWordsBits[i]);
                Console.WriteLine();
                Console.WriteLine(dea.cipherwords[i]);
            }
            string cipher = "";

            for (int i = 0; i < dea.cipherwords.Count(); i++)
            {
                cipher = cipher + (dea.cipherwords[i].ToString());
            }
            Console.WriteLine($"{plaintext} : {cipher}");


            //testing



            //string key = "+~\u0015\u0016(®Ò¦«÷\u0015\u0088\tÏO<\r\n";
            string key1 = "Thats my Kung Fu";
            string text = "Two One Nine Two";
            AES aES = new AES();
            (AES_Algorithm.Model.Round a, RoundWord b, AES_Algorithm.Model.RoundKey c, RoundKeyWord d) round1 =aES.AES_128(text, key1);
            round1.a.DisplayRounds(round1.a.rounds);
            round1.c.DisplayRoundKeys(round1.c.Round);
            Console.WriteLine();
        }

    }
}
