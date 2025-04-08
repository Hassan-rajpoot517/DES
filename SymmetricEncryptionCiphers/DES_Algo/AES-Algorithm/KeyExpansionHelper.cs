using DES_Algo.AES_Algorithm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES_Algo.AES_Algorithm
{
    public class KeyExpansionHelper
    {
        
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
        public RoundKeyWord Convert_Keybits_to_R0(int[] plaintext)
        {
            RoundKeyWord roundkey=new RoundKeyWord();
            string word = "";
            List<int> block = new List<int>();
            int blockSize = 0;
            int wordSize = 0;
            for (int i = 0; i < plaintext.Length; i++)
            {
                block.Add(plaintext[i]);
                blockSize++;
                wordSize++;
                if(wordSize%8==0)
                {
                    int[] arr= new int[wordSize];
                    arr=block.Skip(block.Count - 8).Take(8).ToArray();
                    int num=ConvertBinaryIntoDecimal(arr);
                    word=word+Convert.ToChar(num);
                    wordSize = 0;
                }
                if (blockSize % 32 == 0)
                {
                    int[] arr = new int[blockSize];
                    block.CopyTo(arr, 0);
                    roundkey.Add_RoundKeyWord(word, arr);
                    word = "";
                    blockSize = 0;
                    block.Clear();
                }
            }
            return roundkey;
        }
        public int[] ConvertKeyTextIntoBitsArray(string keytext)
        {
            //if i add padding in the begging of passing string
            int countOfKey=16-keytext.Length;
            keytext=keytext.PadRight(16,'X');
            
            List<int> bitsArray = new List<int>();
            for (int i = 0; i < keytext.Length; i++)
            {
                int[] arr = ConvertCharacterIntoBits(keytext[i],8);
                for (int j = 0; j < arr.Length; j++)
                {
                    bitsArray.Add(arr[j]);
                }
            }
            //if i add 0 in end as required
            int remainingBits=128-bitsArray.Count;
            for (int i = 0; i < remainingBits; i++)
            {
                bitsArray.Add(0);
            }

            int[] resultantArray = new int[bitsArray.Count];
            bitsArray.CopyTo(resultantArray, 0);
            return resultantArray;
        }
        public int[] ConvertCharacterIntoBits(char ch, int numOfRequiredBits)
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
        public (string word, int[] bits) RotWord((string word, int[] bits) Word)
        {
            List<char> characters = Word.word.ToList<char>();
            char letter = characters.First();
            characters.RemoveAt(0);
            characters.Add(letter);
            int[] shiftedBits = LeftCircularShift(Word.bits, 8);
            string word = "";
            for (int i = 0; i < characters.Count; i++)
            {
                word += characters[i];
            }
            return (word, shiftedBits);
        }
        public (int[], int[]) SplitIntoTwoHalves(int[] bits)
        {
            int sizeForHalves = bits.Length / 2;
            (int[] L, int[] R) halves = (new int[sizeForHalves], new int[sizeForHalves]);
            for (int i = 0; i < sizeForHalves; i++)
            {
                halves.L[i] = bits[i];
            }
            int j = sizeForHalves;
            for (int i = 0; i < sizeForHalves; i++)
            {
                halves.R[i] = bits[j];
                j++;
            }
            return halves;
        }
        public int[] LeftCircularShift(int[] bits,int shifts)
        {
            List<int> ShiftedBits=bits.ToList<int>();
            for (int i = 0; i < shifts; i++)
            {
                int bit = ShiftedBits.First();
                ShiftedBits.RemoveAt(0);
                ShiftedBits.Add(bit);
            }
            return ShiftedBits.ToArray();
        }
        public int[] ConvertDecimalIntoBinary(uint num, int numOfBitsRequired)
        {
            List<int> result = new List<int>();
            while (num > 1)
            {
                int bit = (int)num % 2;
                num = num / 2;
                result.Insert(0, bit);
            }
            result.Insert(0, (int)num);
            int remain = numOfBitsRequired - result.Count;
            for (int i = 0; i < remain; i++)
            {
                result.Insert(0, 0);
            }
            int[] arr = new int[numOfBitsRequired];
            result.CopyTo(arr);
            return arr;
        }
        public (string word, int[] bits) Sbox((string word, int[] bits) Word)
        {
            int[,] sbox = new int[16, 16]
            {
                { 99, 124, 119, 123, 242, 107, 111, 197,  48,   1, 103,  43, 254, 215, 171, 118 },
                {202, 130, 201, 125, 250,  89,  71, 240, 173, 212, 162, 175, 156, 164, 114, 192 },
                {183, 253, 147,  38,  54,  63, 247, 204,  52, 165, 229, 241, 113, 216,  49,  21 },
                {  4, 199,  35, 195,  24, 150,   5, 154,   7,  18, 128, 226, 235,  39, 178, 117 },
                {  9, 131,  44,  26,  27, 110,  90, 160,  82,  59, 214, 179,  41, 227,  47, 132 },
                { 83, 209,   0, 237,  32, 252, 177,  91, 106, 203, 190,  57,  74,  76,  88, 207 },
                {208, 239, 170, 251,  67,  77,  51, 133,  69, 249,   2, 127,  80,  60, 159, 168 },
                { 81, 163,  64, 143, 146, 157,  56, 245, 188, 182, 218,  33,  16, 255, 243, 210 },
                {205,  12,  19, 236,  95, 151,  68,  23, 196, 167, 126,  61, 100,  93,  25, 115 },
                { 96, 129,  79, 220,  34,  42, 144, 136,  70, 238, 184,  20, 222,  94,  11, 219 },
                {224,  50,  58,  10,  73,   6,  36,  92, 194, 211, 172,  98, 145, 149, 228, 121 },
                {231, 200,  55, 109, 141, 213,  78, 169, 108,  86, 244, 234, 101, 122, 174,   8 },
                {186, 120,  37,  46,  28, 166, 180, 198, 232, 221, 116,  31,  75, 189, 139, 138 },
                {112,  62, 181, 102,  72,   3, 246,  14,  97,  53,  87, 185, 134, 193,  29, 158 },
                {225, 248, 152,  17, 105, 217, 142, 148, 155,  30, 135, 233, 206,  85,  40, 223 },
                {140, 161, 137,  13, 191, 230,  66, 104,  65, 153,  45,  15, 176,  84, 187,  22 }
            };
            string word = "";
            int[] substitutedBitsArray=new int[Word.bits.Length];
            int k = 0;
            for (int i = 0;i<Word.word.Length;i++)
            {
                (int[] row, int[]column) index=SplitIntoTwoHalves(ConvertCharacterIntoBits(Word.word[i], 8));
                int row = ConvertBinaryIntoDecimal(index.row);
                int col = ConvertBinaryIntoDecimal(index.column);
                int substitutedByte=sbox[row, col];
                word += Convert.ToChar(substitutedByte);
                int[] sByte=ConvertDecimalIntoBinary((uint)substitutedByte, 8);
                for (int j = 0;  j < sByte.Length;  j++)
                {
                    substitutedBitsArray[k] = sByte[j];
                    k++;
                }
            }
            return (word, substitutedBitsArray);
        }
        public int[] XOR(int[] table1, int[] table2)
        {
            int[] output_Table = new int[table1.Length];
            for (int i = 0; i < table1.Length; i++)
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
        public (string word, int[] bits) XoredWithRcon((string word, int[] bits) Word,int round)
        {
            //1 based indexing,10 round hex byte is set not the decimal value for binary(54)
            uint[] RoundConstant = {0,16777216,33554432,67108864,134217728,268435456,
            536870912,1073741824,2147483648,452984832,54};
            int[] RconBits=new int[Word.bits.Length];
            if (round==10)
            {
                int[] arr= ConvertDecimalIntoBinary(RoundConstant[round], 8);
                arr.CopyTo(RconBits, 0);
                for(int i = 0;i < 24;i++)
                {
                    RconBits.Append(0);
                }
            }
            else
            {
                RconBits = ConvertDecimalIntoBinary(RoundConstant[round], 32);
            }
            int[] xoredBits=XOR(Word.bits, RconBits);
            string newWord =MakeWordFrom32Bits(xoredBits);
            return (newWord, xoredBits);
        }
        public string MakeWordFrom32Bits(int[] bits)
        {
            string newWord = "";
            (int[] P1, int[] P2) pairs = SplitIntoTwoHalves(bits);
            (int[] Byte1, int[] Byte2) P1 = SplitIntoTwoHalves(pairs.P1);
            (int[] Byte3, int[] Byte4) P2 = SplitIntoTwoHalves(pairs.P2);

            newWord += Convert.ToChar(ConvertBinaryIntoDecimal(P1.Byte1));
            newWord += Convert.ToChar(ConvertBinaryIntoDecimal(P1.Byte2));
            newWord += Convert.ToChar(ConvertBinaryIntoDecimal(P2.Byte3));
            newWord += Convert.ToChar(ConvertBinaryIntoDecimal(P2.Byte4));
            return newWord;
        }
        public (string word, int[] bits) g((string word, int[] bits) Word,int round)
        {
            (string word, int[] bits) t=RotWord(Word);
            (string word, int[] bits) t1=Sbox(t);
            return XoredWithRcon(t1,round);
        }
        public int ConvertFromHextoDecimal(string hex)
        {
            return Convert.ToInt32 (hex,16);
        }
        public string ConvertFromDecimaltoHex(int num)
        {
            return num.ToString("X");
        }
        public string ConvertBinaryToHex(int[] binaryArray)
        {
            if (binaryArray == null || binaryArray.Length == 0)
                throw new ArgumentException("Input cannot be null or empty.", nameof(binaryArray));
            if (binaryArray.Any(bit => bit != 0 && bit != 1))
                throw new ArgumentException("Input array must contain only 0s and 1s.", nameof(binaryArray));
            string binaryString = string.Join("", binaryArray);
            int decimalValue = Convert.ToInt32(binaryString, 2);
            return decimalValue.ToString("X");
        }
        public RoundKeyWord FillTheBytesInRoundKeyWord(RoundKeyWord roundKeyWord)
        {
            for (int i = 0;i<roundKeyWord.roundkeywords.Count;i++)
            {
                int[] wordBits = roundKeyWord.getter(i).wordBits;
                (int[] P1, int[] P2) pairs=SplitIntoTwoHalves(wordBits);
                (int[] Byte1, int[] Byte2) L = SplitIntoTwoHalves(pairs.P1);
                (int[] Byte3, int[] Byte4) R = SplitIntoTwoHalves(pairs.P2);
                roundKeyWord.Add_BYTE(ConvertBinaryToHex(L.Byte1), L.Byte1);
                roundKeyWord.Add_BYTE(ConvertBinaryToHex(L.Byte2), L.Byte2);
                roundKeyWord.Add_BYTE(ConvertBinaryToHex(R.Byte3), R.Byte3);
                roundKeyWord.Add_BYTE(ConvertBinaryToHex(R.Byte4), R.Byte4);
            }
            return roundKeyWord;
        }

    }
}
