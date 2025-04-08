using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES_Algo.DES_Algorithm.Models
{
    public class Round
    {
        public List<int[]> Rounds=new List<int[]>();
        public void ADD_Round(int[] round) 
        {
            Rounds.Add(round);
        }
        public void Display_Rounds(Round round)
        {
            for (int i = 0; i < round.Rounds.Count; i++)
            {
                Console.Write($"Round {i + 1} :");
                for (int j = 0; j < round.Rounds[i].Length; j++)
                {
                    if (j % 8 == 0)
                        Console.Write(" ");
                    Console.Write(round.Rounds[i][j]);
                }
                Console.WriteLine();
            }
        }
    }
}
