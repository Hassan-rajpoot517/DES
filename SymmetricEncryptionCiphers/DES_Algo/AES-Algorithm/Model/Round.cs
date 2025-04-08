using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES_Algo.AES_Algorithm.Model
{
    public class Round
    {
        public List<string[,]> rounds = new List<string[,]>();
        public Round MakeRoundsFromBytes(List<(string hexaByte, int[] hexaByteBits)> Bytes)
        {
            Round round1 = new Round();
            int ByteCount = 0;
            for (int i = 0; i < Bytes.Count / 16; i++)
            {
                string[,] round = new string[4, 4];

                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        round[j, k] = Bytes[ByteCount].hexaByte;
                        ByteCount++;
                    }
                }
                round1.rounds.Add(round);
            }
            return round1;
        }
        public void AddRound(string[,] round)
        {
            rounds.Add(round);
        }
        public void DisplayRounds(List<string[,]> Round)
        {
            for (int roundIndex = 0; roundIndex < Round.Count; roundIndex++)
            {
                string[,] matrix = Round[roundIndex];
                Console.WriteLine($"Round {roundIndex }:");

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
