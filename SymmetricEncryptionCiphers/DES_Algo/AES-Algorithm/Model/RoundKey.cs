using DES_Algo.DES_Algorithm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES_Algo.AES_Algorithm.Model
{
    public class RoundKey
    {
        public List< string[,]> Round=new List<string[,]>();
        public RoundKey MakeRoundKeysFromBytes(List<(string hexaByte, int[] hexaByteBits)> Bytes)
        {
            RoundKey roundKey1 = new RoundKey();
            int ByteCount = 0;
            for(int i = 0; i < Bytes.Count/16; i++) 
            {
                string[,] roundKey = new string[4, 4];

                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        roundKey[j, k] = Bytes[ByteCount].hexaByte;
                        ByteCount++;
                    }
                }
                roundKey1.Round.Add(roundKey);
            }
            return roundKey1;
        }
        public void DisplayRoundKeys(List<string[,]> Round)
        {
            for (int roundIndex = 0; roundIndex < Round.Count; roundIndex++)
            {
                string[,] matrix = Round[roundIndex];
                Console.WriteLine($"RoundKey {roundIndex }:");

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Console.Write($"{matrix[i, j],4} "); 
                    }
                    Console.WriteLine();
                }

                Console.WriteLine(new string('-', 40)); 
            }
        }

    }
}
