using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES_Algo.DES_Algorithm
{
    public class DES_Round_Helper
    {
        public List<int[]> ConvertPlain_Text_Bits_Into_Blocks(int[] plaintext)
        {
            List<int[]> blocks = new List<int[]>();
            List<int> block = new List<int>();
            int blockSize = 0;
            for (int i = 0; i < plaintext.Length; i++)
            {
                block.Add(plaintext[i]);
                blockSize++;
                if (blockSize % 64 == 0)
                {
                    int[] arr = new int[blockSize];
                    block.CopyTo(arr, 0);
                    blocks.Add(arr);
                    blockSize = 0;
                    block.Clear();
                }
            }
            if (block.Count() > 0)
            {
                for (int i = block.Count(); i < 64; i++)
                {
                    block.Add(0);
                }
                int[] arr = new int[64];
                block.CopyTo(arr, 0);
                blocks.Add(arr);
            }
            return blocks;
        }
        public int[] ConvertPlainTextIntoBitsArray(string plaintext)
        {
            List<int> bitsArray=new List<int>();
            for (int i = 0;i < plaintext.Length;i++)
            {
                int[] arr=ConvertCharacterInto8Bits(plaintext[i]);
                for (int j = 0;j<arr.Length;j++)
                {
                    bitsArray.Add(arr[j]);
                }
            }
            int[] resultantArray=new int[bitsArray.Count];
            bitsArray.CopyTo(resultantArray, 0);
            return resultantArray;
        }  
        public int[] ConvertCharacterInto8Bits(char ch)
        {
            List<int> bits = new List<int>();
            int num = Convert.ToInt32(ch);
            while (num > 1)
            {
                int bit = num % 2;
                num = num / 2;
                bits.Insert(0,bit);
            }
            bits.Insert(0,num);
            int remain = 8 - bits.Count;
            for (int i = 0; i < remain; i++)
            {
                bits.Insert(0,0);
            }
            int[] arr = new int[8];
            bits.CopyTo(arr);
            return arr;
        }
        public int[] InverseInitialPermutation(int[] bits)
        {
            int[] IP_inverse = {
                    39, 7, 47, 15, 55, 23, 63, 31,
                    38, 6, 46, 14, 54, 22, 62, 30,
                    37, 5, 45, 13, 53, 21, 61, 29,
                    36, 4, 44, 12, 52, 20, 60, 28,
                    35, 3, 43, 11, 51, 19, 59, 27,
                    34, 2, 42, 10, 50, 18, 58, 26,
                    33, 1, 41,  9, 49, 17, 57, 25,
                    32, 0, 40,  8, 48, 16, 56, 24};
            int[] arr=new int[IP_inverse.Length];
            for (int i = 0; i < IP_inverse.Length; i++)
            {
                arr[i] = bits[IP_inverse[i]];
            }
            return arr;
        }
        public int[] InitialPermutation(int[] plaintext)
        {
            int[] permutetd_Text=new int[plaintext.Length];
            int[] initial_permutation = { 57, 49, 41, 33, 25, 17, 9, 1,
                              59, 51, 43, 35, 27, 19, 11, 3,
                              61, 53, 45, 37, 29, 21, 13, 5,
                              63, 55, 47, 39, 31, 23, 15, 7,
                              56, 48, 40, 32, 24, 16, 8, 0,
                              58, 50, 42, 34, 26, 18, 10, 2,
                              60, 52, 44, 36, 28, 20, 12, 4,
                              62, 54, 46, 38, 30, 22, 14, 6 };

            for (int i = 0; i < initial_permutation.Length;i++)
            {
                permutetd_Text[i] = plaintext[initial_permutation[i]];
            }
            return permutetd_Text;
        }
        public int[] ExpansionPermutation(int[] R)
        {
            int[] expansionTable = {
                        31,  0,  1,  2,  3,  4,
                        3,  4,  5,  6,  7,  8,
                        7,  8,  9, 10, 11, 12,
                        11, 12, 13, 14, 15, 16,
                        15, 16, 17, 18, 19, 20,
                        19, 20, 21, 22, 23, 24,
                        23, 24, 25, 26, 27, 28,
                        27, 28, 29, 30, 31,  0};
            int[] expanded_Array = new int[expansionTable.Length];
            for(int i = 0;i < expansionTable.Length;i++)
            {
                expanded_Array[i] = R[ expansionTable[i]];
            }
            return expanded_Array;
        }
        public int[] XOR(int[] table1, int[] table2)
        {
            int[] output_Table = new int[table1.Length];
            for(int i=0;i<table1.Length;i++)
            {
                if (table1[i] == table2[i]) 
                {
                    output_Table[i] = 0;
                }
                else
                {
                    output_Table[i] = 1;
                }
            }
            return output_Table;
        }
        public int[] Permuted_Box(int[] array)
        {
            int[] P_Box = {
                            15, 6, 19, 20, 28, 11, 27, 16,
                            0, 14, 22, 25, 4, 17, 30, 9,
                            1, 7, 23, 13, 31, 26, 2, 8,
                            18, 12, 29, 5, 21, 10, 3, 24};
            int[] output_Array = new int[P_Box.Length];
            for(int i=0;i<P_Box.Length;i++)
            {
                output_Array[i] = array[P_Box[i]];
            }
            return output_Array;
        }
        public int ConvertBinaryIntoDecimal(int[] binary)
        {
            int outPut = 0;
            int power = 0;
            for (int i = binary.Length - 1; i >= 0; i--)
            {
                outPut += (Convert.ToInt32(Math.Pow(2, power))) * binary[i];
                power++;
            }
            return outPut;
        }
        public int[] ConvertDecimalIntoBinary(int num,int numOfBitsRequired)
        {
            List<int> result = new List<int>();
            while(num>1)
            {
                int bit = num % 2;
                num = num / 2;
                result.Insert(0,bit);
            }
            result.Insert(0,num);
            int remain=numOfBitsRequired-result.Count;
            for (int i = 0; i < remain; i++)
            {
                result.Insert(0, 0);
            }
            int[] arr = new int[numOfBitsRequired];
            result.CopyTo(arr);
            return arr;
        }
        public int[] ConvertCharacterIntoBits(char ch,int numOfRequiredBits)
        {
            List<int> bits = new List<int>();
            int num = Convert.ToInt32(ch);
            while (num > 1)
            {
                int bit = num % 2;
                num = num / 2;
                bits.Insert(0, bit);
            }
            bits.Insert(0, num);
            int remain = numOfRequiredBits - bits.Count;
            for (int i = 0; i < remain; i++)
            {
                bits.Insert(0, 0);
            }
            int[] arr = new int[8];
            bits.CopyTo(arr);
            return arr;
        }
        public List<int[]> ConvertBitsIntoBatches(int[] bits,int batchSize)
        {
            List<int[]> blocks = new List<int[]>();
            List<int> block = new List<int>();
            int blockSize = 0;
            for (int i = 0; i < bits.Length; i++)
            {
                block.Add(bits[i]);
                blockSize++;
                if (blockSize % batchSize == 0)
                {
                    int[] arr = new int[blockSize];
                    block.CopyTo(arr, 0);
                    blocks.Add(arr);
                    blockSize = 0;
                    block.Clear();
                }
            }
            if (block.Count() > 0)
            {
                for (int i = block.Count(); i < batchSize; i++)
                {
                    block.Add(0);
                }
                int[] arr = new int[batchSize];
                block.CopyTo(arr, 0);
                blocks.Add(arr);
            }
            return blocks;
        }
        public (int Row,int Coulomb) ConvertIntoMatrixIndexes(int[] batch)
        {
            (int row, int col) index = (new int(), new int());
            int[] row = new int[2];
            int[] col= new int[4];
            row[0] = batch[0];
            row[1] = batch[batch.Length-1];
            for(int i=0; i < 4; i++) 
            {
                col[i] = batch[i + 1];
            }
            index.row=ConvertBinaryIntoDecimal(row);
            index.col=ConvertBinaryIntoDecimal(col);
            return index;
        }
        public List<(int ,int )> Get_S_Box_Indexes(int[] bits,int batchSize)
        {
            List<int[]> batches=ConvertBitsIntoBatches(bits, batchSize);
            List<(int,int)> indexes=new List<(int,int)>();
            for(int i=0;i<batches.Count;i++)
            {
                (int, int) index = ConvertIntoMatrixIndexes(batches[i]);
                indexes.Add(index);
            }
            return indexes;
        }
        public int[] S_Box(int[] unSubtitutedBits)
        {
            List<int[,]> s_boxes = new List<int[,]>();
            int[,] S1 = {
            { 14,  4, 13,  1,  2, 15, 11,  8,  3, 10,  6, 12,  5,  9,  0,  7 },
            {  0, 15,  7,  4, 14,  2, 13,  1, 10,  6, 12, 11,  9,  5,  3,  8 },
            {  4,  1, 14,  8, 13,  6,  2, 11, 15, 12,  9,  7,  3, 10,  5,  0 },
            { 15, 12,  8,  2,  4,  9,  1,  7,  5, 11,  3, 14, 10,  0,  6, 13 }
        };
            int[,] S2 = {
            { 15,  1,  8, 14,  6, 11,  3,  4,  9,  7,  2, 13, 12,  0,  5, 10 },
            {  3, 13,  4,  7, 15,  2,  8, 14, 12,  0,  1, 10,  6,  9, 11,  5 },
            {  0, 14,  7, 11, 10,  4, 13,  1,  5,  8, 12,  6,  9,  3,  2, 15 },
            { 13,  8, 10,  1,  3, 15,  4,  2, 11,  6,  7, 12,  0,  5, 14,  9 }
        };
            int[,] S3 = {
            { 10,  0,  9, 14,  6,  3, 15,  5,  1, 13, 12,  7, 11,  4,  2,  8 },
            { 13,  7,  0,  9,  3,  4,  6, 10,  2,  8,  5, 14, 12, 11, 15,  1 },
            { 13,  6,  4,  9,  8, 15,  3,  0, 11,  1,  2, 12,  5, 10, 14,  7 },
            {  1, 10, 13,  0,  6,  9,  8,  7,  4, 15, 14,  3, 11,  5,  2, 12 }
        };
            int[,] S4 = {
            {  7, 13, 14,  3,  0,  6,  9, 10,  1,  2,  8,  5, 11, 12,  4, 15 },
            { 13,  8, 11,  5,  6, 15,  0,  3,  4,  7,  2, 12,  1, 10, 14,  9 },
            { 10,  6,  9,  0, 12, 11,  7, 13, 15,  1,  3, 14,  5,  2,  8,  4 },
            {  3, 15,  0,  6, 10,  1, 13,  8,  9,  4,  5, 11, 12,  7,  2, 14 }
        };
            int[,] S5 = {
            {  2, 12,  4,  1,  7, 10, 11,  6,  8,  5,  3, 15, 13,  0, 14,  9 },
            { 14, 11,  2, 12,  4,  7, 13,  1,  5,  0, 15, 10,  3,  9,  8,  6 },
            {  4,  2,  1, 11, 10, 13,  7,  8, 15,  9, 12,  5,  6,  3,  0, 14 },
            { 11,  8, 12,  7,  1, 14,  2, 13,  6, 15,  0,  9, 10,  4,  5,  3 }
        };
            int[,] S6 = {
            { 12,  1, 10, 15,  9,  2,  6,  8,  0, 13,  3,  4, 14,  7,  5, 11 },
            { 10, 15,  4,  2,  7, 12,  9,  5,  6,  1, 13, 14,  0, 11,  3,  8 },
            {  9, 14, 15,  5,  2,  8, 12,  3,  7,  0,  4, 10,  1, 13, 11,  6 },
            {  4,  3,  2, 12,  9,  5, 15, 10, 11, 14,  1,  7,  6,  0,  8, 13 }
        };
            int[,] S7 = {
            {  4, 11,  2, 14, 15,  0,  8, 13,  3, 12,  9,  7,  5, 10,  6,  1 },
            { 13,  0, 11,  7,  4,  9,  1, 10, 14,  3,  5, 12,  2, 15,  8,  6 },
            {  1,  4, 11, 13, 12,  3,  7, 14, 10, 15,  6,  8,  0,  5,  9,  2 },
            {  6, 11, 13,  8,  1,  4, 10,  7,  9,  5,  0, 15, 14,  2,  3, 12 }
        };
            int[,] S8 = {
            { 13,  2,  8,  4,  6, 15, 11,  1, 10,  9,  3, 14,  5,  0, 12,  7 },
            {  1, 15, 13,  8, 10,  3,  7,  4, 12,  5,  6, 11,  0, 14,  9,  2 },
            {  7, 11,  4,  1,  9, 12, 14,  2,  0,  6, 10, 13, 15,  3,  5,  8 },
            {  2,  1, 14,  7,  4, 10,  8, 13, 15, 12,  9,  0,  3,  5,  6, 11 }
        };
            s_boxes.Add(S1);
            s_boxes.Add(S2);
            s_boxes.Add(S3);
            s_boxes.Add(S4);
            s_boxes.Add(S5);
            s_boxes.Add(S6);
            s_boxes.Add(S7);
            s_boxes.Add(S8);
            List<int[]> batches = ConvertBitsIntoBatches(unSubtitutedBits, 6);
            List<(int, int)> indexes = Get_S_Box_Indexes(unSubtitutedBits, 6);
            List<int> output= new List<int>();
            for (int i = 0; i < batches.Count; i++)
            {
                int outputNum = s_boxes[i][indexes[i].Item1, indexes[i].Item2];
                int[] requiredBatch=ConvertDecimalIntoBinary(outputNum, 4);
                for (int j = 0; j < requiredBatch.Length; j++)
                {
                    output.Add(requiredBatch[j]);
                }
            }
            int[] arr= new int[output.Count];
            output.CopyTo(arr, 0);
            return arr;
        }
        public (int[], int[]) SplitIntoTwoHalves(int[] bits)
        {
            int sizeForHalves=bits.Length/2;
            (int[] L, int[] R) halves = (new int[sizeForHalves],new int[sizeForHalves]);
            for(int i = 0;i < sizeForHalves;i++)
            {
                halves.L[i] = bits[i];
            }
            int j = 32;
            for(int i = 0;i < sizeForHalves;i++)
            {
                halves.R[i] = bits[j];
                j++;
            }
            return halves;
        }
        public int[] ManglerFunction(int[] bits, int[] roundKey)
        {
            int[] expanded_bits=ExpansionPermutation(bits);
            int[] xored=XOR(expanded_bits,roundKey);
            int[] subsitutedBits = S_Box(xored);
            int[] permutedBits=Permuted_Box(subsitutedBits);
            return permutedBits;
        }
        public int[] CombineTwoHalves(int[] L, int[] R)
        {
            int[] array=new int[L.Length+R.Length];
            int j = L.Length;
            for(int i = 0; i < L.Length; i++)
            {
                array[i] = L[i];
                array[j] = R[i];
                j++;
            }
            return array;
        }
        public int[] Swap32Bits(int[] bits)
        {
            int j = bits.Length/2;
            for (int i = 0; i < bits.Length/2; i++)
            {
                int temp = bits[i];
                bits[i] = bits[j];
                bits[j] = temp;
                j++;
            }
            return bits;
        }
        public string ConvertBitsIntoCiphertext(int[] bits)
        {
            string ciphertext="";
            List<int> ch=new List<int>();
            int chSize = 0;
            for(int i=0; i < bits.Length; i++)
            {
                ch.Add(bits[i]);
                chSize++;
                if(chSize%8 == 0)
                {
                    int[] arr=new int[8];
                    ch.CopyTo(arr, 0);
                    ch.Clear();
                    ciphertext+=Convert.ToChar(ConvertBinaryIntoDecimal(arr));
                }
            }
            return ciphertext.Trim();
        }
        public void DisplayCipherBits(List<int[]> cipherbits)
        {
            for (int i = 0; i < cipherbits.Count; i++)
            {
                Console.Write($"Block {i + 1}: ");
                for (int j = 0; j < cipherbits[i].Length; j++)
                {
                    Console.Write(cipherbits[i][j]); 
                    if ((j + 1) % 8 == 0) Console.Write(" "); 
                }
                Console.WriteLine(); 
            }
        }
        public void Display64BitArray(int[] bits)
        {
            
            for (int i = 0; i < bits.Length; i++)
            {
                Console.Write(bits[i].ToString().PadLeft(2, ' ') + " ");
                if ((i + 1) % 8 == 0) 
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
    }

}
