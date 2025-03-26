using DES_Algo.DES_Algorithm;
using DES_Algo.DES_Algorithm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES_Algo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            DES dES = new DES();
            DES_Round_Helper helper = new DES_Round_Helper();
            string plaintext= "hassan tanveer rajpoot janjua";
            string key = "rajpoot";//key 8 character sy ziada na dena warna error aye
            (List<Round> rounds, RoundKey roundKeys, List<int[]> cipherWordsBits,List<string> cipherwords) dea
                = dES.DEA(plaintext, key);
            RoundKey roundKey = new RoundKey();
            Round round = new Round();
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
                Console.WriteLine() ;
                Console.WriteLine(dea.cipherwords[i]);
            }
            string cipher = "";

            for (int i = 0;i<dea.cipherwords.Count();i++)
            {
                cipher=cipher+(dea.cipherwords[i].ToString());
            }
            Console.WriteLine($"{plaintext} : {cipher}");
        }
        
    }
}
