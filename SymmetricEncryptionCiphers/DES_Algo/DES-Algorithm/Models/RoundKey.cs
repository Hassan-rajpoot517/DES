using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES_Algo.DES_Algorithm.Models
{
    public class RoundKey
    {
        public List<int[]> RoundKeys=new List<int[]>();
        public void ADD_RoundKey(int[] roundKey)
        {
            RoundKeys.Add(roundKey);
        }
        public void Display_RoundKeys(RoundKey roundKey)
        {
            for (int i = 0; i < roundKey.RoundKeys.Count; i++)
            {
                Console.Write($"RoundKey {i+1} :");
                for(int j = 0; j < roundKey.RoundKeys[i].Length; j++)
                {
                    if (j % 8 == 0)
                        Console.Write(" ");
                    Console.Write(roundKey.RoundKeys[i][j]);
                }
                Console.WriteLine();
            }
        }
    }
}
