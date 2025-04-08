using DES_Algo.DES_Algorithm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES_Algo.DES_Algorithm
{
    public class DES
    {
        public (List<Round> TotalWords,RoundKey KeyS, List<int[]> CipherBits,List<string> CipherWords) DEA(string plaintext, string key)
        {
            List<string> cipherText = new List<string>();
            List<int[]> cipherBits = new List<int[]>();
            RoundKeysGenerator roundKeysGenerator = new RoundKeysGenerator();
            List<Round> rounds=new List<Round>();
            DES_Round_Helper round_Helper = new DES_Round_Helper();
            RoundKey round_Keys=roundKeysGenerator.GenerateRoundKeys(key);
            int[] plaintext_bits=round_Helper.ConvertPlainTextIntoBitsArray(plaintext);
            List<int[]> blocks=round_Helper.ConvertPlain_Text_Bits_Into_Blocks(plaintext_bits);
            for (int i = 0;i<blocks.Count;i++)
            {
                Round roundAccordingToBlock = new Round();
                int[] InitialPermutedArray=round_Helper.InitialPermutation(blocks[i]);
                (int[] L, int[] R) halves=round_Helper.SplitIntoTwoHalves(InitialPermutedArray);
                for (int j = 0; j < round_Keys.RoundKeys.Count; j++)
                {
                    int[] outPutOfMgFun=round_Helper.ManglerFunction(halves.R, round_Keys.RoundKeys[j]);
                    int[] temp = halves.R;
                    halves.R = round_Helper.XOR(halves.L, outPutOfMgFun);
                    halves.L = temp; 
                    int[] round = round_Helper.CombineTwoHalves(halves.L, halves.R);
                    roundAccordingToBlock.ADD_Round(round);
                }
                rounds.Add(roundAccordingToBlock);
            }
            for (int i = 0; i < rounds.Count; i++) 
            { 
                int[] finalRound=round_Helper.Swap32Bits(rounds[i].Rounds[rounds[i].Rounds.Count - 1]);
                int[] cipherbits=round_Helper.InverseInitialPermutation(finalRound); 
                cipherBits.Add(cipherbits);
                cipherText.Add(round_Helper.ConvertBitsIntoCiphertext(cipherbits));
            }
            return (rounds,round_Keys,cipherBits,cipherText);
        }
    }
}
