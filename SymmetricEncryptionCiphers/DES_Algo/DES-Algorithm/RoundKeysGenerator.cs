using DES_Algo.DES_Algorithm;
using DES_Algo.DES_Algorithm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES_Algo.DES_Algorithm
{
    public class RoundKeysGenerator
    {
        public RoundKey GenerateRoundKeys(string keytext)
        {
            RoundKey roundKey = new RoundKey();
            DES_KeyScheduling_Helper key_Helper = new DES_KeyScheduling_Helper();
            int[] shifts = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
            int[] bitsArray=key_Helper.ConvertkeyTextIntoBitsArray(keytext);
            int[] permutedArray=key_Helper.PermutedChoice_1(bitsArray);
            (int[], int[]) CD=key_Helper.Convert_PC1_Into_TwoHalves(permutedArray);
            
            for (int i = 0;i<16;i++)
            {
                (int[], int[]) nextCD=key_Helper.LeftCircularShift(CD, shifts[i]);
                int[] round_key=key_Helper.PermutedChoice_2(nextCD);
                roundKey.ADD_RoundKey(round_key);
                CD = nextCD;
            }
            return roundKey;
        }
    }
}
