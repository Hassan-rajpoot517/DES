using DES_Algo.AES_Algorithm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DES_Algo.AES_Algorithm
{
    public class RoundHelper
    {
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
        public int[] ConvertKeyTextIntoBitsArray(string plaintext)
        {
            //if i add padding in the begging of passing string
            int countOfKey = 16 - plaintext.Length;
            plaintext = plaintext.PadRight(16, 'X');

            List<int> bitsArray = new List<int>();
            for (int i = 0; i < plaintext.Length; i++)
            {
                int[] arr = ConvertCharacterIntoBits(plaintext[i], 8);
                for (int j = 0; j < arr.Length; j++)
                {
                    bitsArray.Add(arr[j]);
                }
            }
            //if i add 0 in end as required
            int remainingBits = 128 - bitsArray.Count;
            for (int i = 0; i < remainingBits; i++)
            {
                bitsArray.Add(0);
            }

            int[] resultantArray = new int[bitsArray.Count];
            bitsArray.CopyTo(resultantArray, 0);
            return resultantArray;
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
        public RoundWord Convert_Plainbits_to_StateMatrix(int[] plaintext)
        {
            RoundWord roundword = new RoundWord();
            string word = "";
            List<int> block = new List<int>();
            int blockSize = 0;
            int wordSize = 0;
            for (int i = 0; i < plaintext.Length; i++)
            {
                block.Add(plaintext[i]);
                blockSize++;
                wordSize++;
                if (wordSize % 8 == 0)
                {
                    int[] arr = new int[wordSize];
                    arr = block.Skip(block.Count - 8).Take(8).ToArray();
                    int num = ConvertBinaryIntoDecimal(arr);
                    word = word + Convert.ToChar(num);
                    wordSize = 0;
                }
                if (blockSize % 32 == 0)
                {
                    int[] arr = new int[blockSize];
                    block.CopyTo(arr, 0);
                    roundword.Add_RoundWord(word, arr);
                    word = "";
                    blockSize = 0;
                    block.Clear();
                }
            }
            return roundword;
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
        public RoundWord FillTheBytesInRoundWord(RoundWord roundWord)
        {
            for (int i = 0; i < roundWord.roundwords.Count; i++)
            {
                int[] wordBits = roundWord.getter(i).wordBits;
                (int[] P1, int[] P2) pairs = SplitIntoTwoHalves(wordBits);
                (int[] Byte1, int[] Byte2) L = SplitIntoTwoHalves(pairs.P1);
                (int[] Byte3, int[] Byte4) R = SplitIntoTwoHalves(pairs.P2);
                roundWord.Add_BYTE(ConvertBinaryToHex(L.Byte1), L.Byte1);
                roundWord.Add_BYTE(ConvertBinaryToHex(L.Byte2), L.Byte2);
                roundWord.Add_BYTE(ConvertBinaryToHex(R.Byte3), R.Byte3);
                roundWord.Add_BYTE(ConvertBinaryToHex(R.Byte4), R.Byte4);
            }
            return roundWord;
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
            int[] substitutedBitsArray = new int[Word.bits.Length];
            int k = 0;
            for (int i = 0; i < Word.word.Length; i++)
            {
                (int[] row, int[] column) index = SplitIntoTwoHalves(ConvertCharacterIntoBits(Word.word[i], 8));
                int row = ConvertBinaryIntoDecimal(index.row);
                int col = ConvertBinaryIntoDecimal(index.column);
                int substitutedByte = sbox[row, col];
                word += Convert.ToChar(substitutedByte);
                int[] sByte = ConvertDecimalIntoBinary((uint)substitutedByte, 8);
                for (int j = 0; j < sByte.Length; j++)
                {
                    substitutedBitsArray[k] = sByte[j];
                    k++;
                }
            }
            return (word, substitutedBitsArray);
        }
        public int[] LeftCircularShift(int[] bits, int shifts)
        {
            List<int> ShiftedBits = bits.ToList<int>();
            for (int i = 0; i < shifts; i++)
            {
                int bit = ShiftedBits.First();
                ShiftedBits.RemoveAt(0);
                ShiftedBits.Add(bit);
            }
            return ShiftedBits.ToArray();
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
        //***********
        public RoundWord AddRoundKey(RoundWord roundWord,RoundKeyWord roundKeyWord,int roundKeyStartingIndex)
        {
            int index=roundKeyStartingIndex;
            RoundWord output = new RoundWord();
            for (int i = 0; i < roundWord.roundwords.Count; i++)
            {
                int[] wordBits=XOR(roundWord.roundwords[i].wordBits, roundKeyWord.roundkeywords[index].wordBits);
                string word = MakeWordFrom32Bits(wordBits);
                output.Add_RoundWord(word,wordBits);
                index++;
            }
            return output;
        }
        public RoundWord SubstiteBytes(RoundWord roundWord)
        {
            RoundWord output = new RoundWord();
            for(int i = 0;i < roundWord.roundwords.Count;i++)
            {
                (string word, int[] bits) outputword=Sbox(roundWord.roundwords[i]);
                output.Add_RoundWord(outputword.word,outputword.bits);
            }
            return output;
        }
        public RoundWord ShiftRows(RoundWord roundWord)
        {
            //mera roundword wala object ma word bytes wala matrix ka hisab sa column wise para howa tha ,
            // jis pr shiftrows galat kam kr raha tha kio ka mera words ki orientation matrix ka words ka hisab sa nhi thi 
            //is lia mena pahla roundword ka object ma bytes matrix ka hisab sa kr li hir is pr ShiftRows apply kia 
            //kio ka ma to words , bytes wala matrix ka hisab sa columns ma deal kr raha to akhir ma wapis apna original orientation ma
            //la aya words ko....umeed krta hun samjh a gaya ho ga
            RoundWord roundWord1 = new RoundWord();
            //i am changing my words orientation so that shiftrows works proper onit
            for(int i = 0;i<roundWord.roundwords.Count;i++)
            {
                string word0 = "";
                List<int> bits = new List<int>();

                for (int j=0;j<roundWord.roundwords.Count;j++)
                {
                    char ch = roundWord.roundwords[j].word[i];
                    bits.AddRange(ConvertCharacterIntoBits(ch, 8));
                    word0 += ch;
                }
                roundWord1.Add_RoundWord(word0, bits.ToArray());                
            }
            RoundWord output = new RoundWord();
            for(int i = 0;i<roundWord1.roundwords.Count ; i++)
            {
                if(i==0)
                {
                    output.Add_RoundWord(roundWord1.roundwords[i].word, roundWord1.roundwords[i].wordBits);
                }
                if(i==1)
                {
                    (string word, int[] bits) outputword = RotWord(roundWord1.roundwords[i]);
                    output.Add_RoundWord(outputword.word, outputword.bits);
                }
                if(i==2)
                {
                    (string word, int[] bits) outputword=RotWord(RotWord(roundWord1.roundwords[i]));
                    output.Add_RoundWord(outputword.word, outputword.bits);
                }
                if(i==3)
                {
                    (string word, int[] bits) outputword = RotWord(RotWord(RotWord(roundWord1.roundwords[i])));
                    output.Add_RoundWord(outputword.word, outputword.bits);
                }
            }
            RoundWord finalOutput=new RoundWord();
            for (int i = 0; i < output.roundwords.Count; i++)
            {
                string word0 = "";
                List<int> bits = new List<int>();

                for (int j = 0; j < output.roundwords.Count; j++)
                {
                    char ch = output.roundwords[j].word[i];
                    bits.AddRange(ConvertCharacterIntoBits(ch, 8));
                    word0 += ch;
                }
                finalOutput.Add_RoundWord(word0, bits.ToArray());
            }
            return finalOutput;
        }
        public char MultiplyWithHex02(char ch)
        {
            int[] arr = ConvertCharacterIntoBits(ch, 8);
            int MSB = arr[0];
            for (int i = 0; i < 7; i++)
            {
                arr[i] = arr[i + 1];
            }
            arr[7] = 0;
            if (MSB == 1)
            {
                int[] rPoly = ConvertDecimalIntoBinary(0x1B, 8);
                arr = XOR(arr, rPoly);
            }
            return Convert.ToChar(ConvertBinaryIntoDecimal(arr));
        }
        public char MultiplyWithHex03(char ch)
        {
           return Convert.ToChar(ConvertBinaryIntoDecimal
               (XOR(ConvertCharacterIntoBits(MultiplyWithHex02(ch), 8),
               ConvertCharacterIntoBits(ch,8))));            
        }
        public RoundWord MixColumn(RoundWord roundWord)
        {
            List<string[]> mixColumnbMatrix = new List<string[]>();
            string[] row1 = { "02", "03", "01", "01" };
            string[] row2 = { "01", "02", "03", "01" };
            string[] row3 = { "01", "01", "02", "03" };
            string[] row4 = { "03", "01", "01", "02" };
            mixColumnbMatrix.Add(row1);
            mixColumnbMatrix.Add(row2);
            mixColumnbMatrix.Add(row3);
            mixColumnbMatrix.Add(row4);

            RoundWord output = new RoundWord();
            for (int k = 0; k < mixColumnbMatrix.Count; k++)
            {
                List<char> characterForWord = new List<char>();
                for (int i = 0; i < 4; i++)
                {
                    List<char> chars = new List<char>();

                    for (int j = 0; j < roundWord.roundwords.Count; j++)
                    {
                        if (mixColumnbMatrix[k][j] == "01")
                        {
                            chars.Add(roundWord.roundwords[i].word[j]);
                        }
                        if (mixColumnbMatrix[k][j] == "02")
                        {
                            chars.Add(MultiplyWithHex02(roundWord.roundwords[i].word[j]));
                        }
                        if (mixColumnbMatrix[k][j] == "03")
                        {
                            chars.Add(MultiplyWithHex03(roundWord.roundwords[i].word[j]));
                        }
                    }
                    int[] temp=XOR(ConvertCharacterIntoBits(chars[0], 8),ConvertCharacterIntoBits(chars[1], 8));
                    int[] bits=XOR(temp, XOR(ConvertCharacterIntoBits(chars[2], 8), ConvertCharacterIntoBits(chars[3], 8)));
                    characterForWord.Add(Convert.ToChar(ConvertBinaryIntoDecimal(bits)));
                }
                List<int> wordBits=new List<int>();
                for (int j = 0;j<characterForWord.Count;j++)
                {
                    wordBits.AddRange(ConvertCharacterIntoBits(characterForWord[j], 8));
                }
                string word=MakeWordFrom32Bits(wordBits.ToArray());
                output.Add_RoundWord(word, wordBits.ToArray());
            }
            //pata nhi kio mera object ka words col sa row ma convert ho gay hain is lia dubara sidha kia ha
            RoundWord finalOutput = new RoundWord();
            for (int i = 0; i < output.roundwords.Count; i++)
            {
                string word0 = "";
                List<int> bits = new List<int>();

                for (int j = 0; j < output.roundwords.Count; j++)
                {
                    char ch = output.roundwords[j].word[i];
                    bits.AddRange(ConvertCharacterIntoBits(ch, 8));
                    word0 += ch;
                }
                finalOutput.Add_RoundWord(word0, bits.ToArray());
            }
            return finalOutput;
        }

    }
}
