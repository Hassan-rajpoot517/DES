using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES_Algo.DES_Algorithm
{
    public class DES_KeyScheduling_Helper
    {
        public List<int[]> ConvertKey_Text_Bits_Into_Blocks(int[] key)
        {
            List<int[]> blocks = new List<int[]>();
            List<int> block = new List<int>();
            int blockSize = 0;
            for (int i = 0; i < key.Length; i++)
            {
                block.Add(key[i]);
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
        public int[] ConvertkeyTextIntoBitsArray(string key)
        {
            List<int> bitsArray = new List<int>();
            for (int i = 0; i < key.Length; i++)
            {
                int[] arr = ConvertCharacterInto8Bits(key[i]);
                for (int j = 0; j < arr.Length; j++)
                {
                    bitsArray.Add(arr[j]);
                }
            }
            int remain = 64-bitsArray.Count;
            for(int i=0;i<remain;i++)
            {
                bitsArray.Add(0);
            }
            int[] resultantArray = new int[bitsArray.Count];
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
                bits.Insert(0, bit);
            }
            bits.Insert(0, num);
            int remain = 8 - bits.Count;
            for (int i = 0; i < remain; i++)
            {
                bits.Insert(0, 0);
            }
            int[] arr = new int[8];
            bits.CopyTo(arr);
            return arr;
        }
        public int[] PermutedChoice_1(int[] key)
        {
            int[] permuted_Choice1_NZ = { 57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18,
                                        10,  2, 59, 51, 43, 35, 27,19, 11,  3, 60, 52, 44, 36,
                                        63, 55, 47, 39, 31, 23, 15, 7, 62, 54, 46, 38, 30, 22,
                                        14,  6, 61, 53, 45, 37, 29, 21, 13,  5, 28, 20, 12,  4};
            int[] permuted_Choice1=new int[56];
            for (int i = 0;i<permuted_Choice1_NZ.Length;i++)
            {
                permuted_Choice1[i] = permuted_Choice1_NZ[i]-1;
            }
            int[] output_PC1 = new int[permuted_Choice1.Length];
            for (int i = 0; i < permuted_Choice1.Length; i++)
            {
                output_PC1[i] = key[permuted_Choice1[i]];
            }
            return output_PC1;
        }
        public (int[], int[]) Convert_PC1_Into_TwoHalves(int[] pc1)
        {
            (int[] C, int[] D) arrays = (new int[28],new int[28]);
            for(int i = 0;i<28;i++)
            {
                arrays.C[i] = pc1[i];
            }
            int j = 28;
            for (int i = 0; i < 28; i++)
            {
                arrays.D[i] = pc1[j];
                j++;
            }
            return arrays;
        }
        public (int[], int[]) LeftCircularShift((int[], int[]) arrays,int shifts)
        {
            (int[] C, int[] D) output = (new int[28],new int[28]);
            for(int i = 0;i< shifts;i++)
            {
                int c1, d1;
                c1=arrays.Item1[0];
                d1= arrays.Item2[0];
                for(int j=0;j<27;j++)
                {
                    arrays.Item1[j] = arrays.Item1[j+1];
                }
                arrays.Item1[27]=c1;
                for(int j=0;j<27;j++)
                {
                    arrays.Item2[j]=arrays.Item2[j+1];
                }
                arrays.Item2[27]=d1;
            }
            output = arrays;
            return output;
        }
        public int[] PermutedChoice_2((int[], int[]) arrays )
        {
            int[] combinedArray=new int[56];
            int[] permutedChoice_2 = { 13, 16, 10, 23, 0, 4, 2, 27, 14, 5, 20, 9,
                            22, 18, 11, 3, 25, 7, 15, 6, 26, 19, 12, 1,
                            40, 51, 30, 36, 46, 54, 29, 39, 50, 44, 32, 47,
                            43, 48, 38, 55, 33, 52, 45, 41, 49, 35, 28, 31 };

            int[] output=new int[permutedChoice_2.Length];
            for(int i = 0; i<28; i++)
            {
                combinedArray[i] = arrays.Item1[i];
            }
            int j = 28;
            for (int i = 0; i <28; i++)
            {
                combinedArray[j] = arrays.Item2[i];
                j++;
            }

            for (int i = 0; i < permutedChoice_2.Length; i++)
            {
                output[i] = combinedArray[permutedChoice_2[i]];
            }
            return output;
        }
    }
}
