using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES_Algo.AES_Algorithm.Model
{
    public class RoundWord
    {
        public List<(string word, int[] wordBits)> roundwords = new List<(string word, int[] wordBits)>();
        public List<(string hexaByte, int[] hexaByteBits)> wordBYTES = new List<(string hexaByte, int[] hexaByteBits)>();
        public (string text, int[] bits) PlainText { get; set; }
        public (string word, int[] wordBits) getter(int index)
        {
            return roundwords[index];
        }
        public void ClearRoundWords()
        {
            roundwords.Clear();

        }
        public (string hexaByte, int[] bits) getByte(int index)
        {
            return wordBYTES[index];
        }
        public void Add_RoundWord(string word, int[] wordsbit)
        {
            roundwords.Add((word, wordsbit));
        }
        public void Add_BYTE(string Byte, int[] BYTEbit)
        {
            wordBYTES.Add((Byte, BYTEbit));
        }
        public void Display(RoundWord roundword, bool ByteRepresentation)
        {
            int BytesCount = 0;

            for (int i = 0; i < roundword.roundwords.Count; i++)
            {
                var (word, wordBits) = roundword.roundwords[i];

                // Print word label and bits
                Console.Write($"W{i} : {word}  ");
                DisplayBits(wordBits);
                Console.WriteLine();

                // Optionally display bytes
                if (ByteRepresentation)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        DisplayByte(BytesCount);
                        BytesCount++;
                    }

                    Console.WriteLine(); // extra line between each word
                }
            }
        }
        public void DisplayByte(int index)
        {
            if (index >= wordBYTES.Count)
                return;

            var (hexaByte, hexaByteBits) = wordBYTES[index];

            Console.Write("     "); // Indent to align under word
            Console.Write($"{hexaByte.PadRight(3)} ");
            DisplayBits(hexaByteBits);
            Console.WriteLine();
        }
        void DisplayBits(int[] bits)
        {
            for (int i = 0; i < bits.Length; i++)
            {
                if (i % 4 == 0 && i != 0)
                    Console.Write(" ");

                Console.Write(bits[i]);
            }
        }

    }
}
