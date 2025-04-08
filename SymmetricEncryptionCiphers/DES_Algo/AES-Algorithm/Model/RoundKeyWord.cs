using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES_Algo.AES_Algorithm.Model
{
    public class RoundKeyWord
    {
        public List<(string word, int[] wordBits)> roundkeywords = new List<(string word, int[] wordBits)>();
        public List<(string hexaByte, int[] hexaByteBits)> BYTES=new List<(string hexaByte, int[] hexaByteBits)>();
        public (string word, int[] wordBits) getter(int index)
        {
            return roundkeywords[index];
        }
        public (string hexaByte, int[] bits) getByte(int index)
        {
            return BYTES[index];
        }
        public void Add_RoundKeyWord(string word, int[] wordsbit)
        {
            roundkeywords.Add((word, wordsbit));
        }
        public void Add_BYTE(string Byte, int[] BYTEbit)
        {
            BYTES.Add((Byte, BYTEbit));
        }
        //public void Display(RoundKeyWord roundKeys,bool ByteRepresentation)
        //{
        //    int BytesCount = 0;
        //    for(int i = 0; i < roundKeys.roundkeywords.Count; i++)
        //    {
        //        Console.Write($"W{i} :{roundKeys.roundkeywords[i].word}");
        //        DisplayBits(roundKeys.roundkeywords[i].wordBits);
        //        if (ByteRepresentation) 
        //        { 
        //            for (int j = 0; j < 4; j++)
        //            {
        //                DisplayByte(BytesCount);
        //                BytesCount++;
        //            }
        //        }
        //        Console.WriteLine();    
        //    }
        //} 
        //public void DisplayByte(int index)
        //{
        //    Console.Write($"  {BYTES[index].hexaByte} ");
        //    DisplayBits(BYTES[index].hexaByteBits);
        //}
        //void DisplayBits(int[] bits) 
        //{
        //    for (int i = 0; i < bits.Length; i++)
        //    {
        //        if (i % 4 == 0)
        //            Console.Write(" ");
        //        Console.Write(bits[i]);
                
        //    }
        //}
        //*************************
        public void Display(RoundKeyWord roundKeys, bool ByteRepresentation)
        {
            int BytesCount = 0;

            for (int i = 0; i < roundKeys.roundkeywords.Count; i++)
            {
                var (word, wordBits) = roundKeys.roundkeywords[i];

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
            if (index >= BYTES.Count)
                return;

            var (hexaByte, hexaByteBits) = BYTES[index];

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
