using DES_Algo.AES_Algorithm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES_Algo.AES_Algorithm
{
    public class RoundKeyGenerator
    {
        public (RoundKeyWord roundWords,RoundKey roundKeys) GenerateKeys(string key)
        {
            KeyExpansionHelper keyExpansionHelper = new KeyExpansionHelper();
            RoundKeyWord keyWords = new RoundKeyWord();
            keyWords=keyExpansionHelper.Convert_Keybits_to_R0(keyExpansionHelper.ConvertKeyTextIntoBitsArray(key));
            int round = 1;
            for (int i = 4;i<44;i++)
            {
                (string word, int[] bits) roundWord =("", new int[32]);
                if(i%4==0)
                {
                    (string word, int[] bits) temp = keyWords.getter(i - 1);
                    (string word, int[] bits) temp1=keyExpansionHelper.g(temp,round);
                    roundWord.bits=keyExpansionHelper.XOR(keyWords.getter(i - 4).wordBits,temp1.bits);
                    string word=keyExpansionHelper.MakeWordFrom32Bits(roundWord.bits);
                    roundWord.word=word;
                    keyWords.Add_RoundKeyWord(roundWord.word, roundWord.bits);
                    round++;
                }
                else
                {
                    (string word, int[] bits) previous=keyWords.getter(i-1);
                    (string word, int[] bits) FromPreviousRound=keyWords.getter(i - 4);
                    int[] bits=keyExpansionHelper.XOR(previous.bits, FromPreviousRound.bits);
                    roundWord.bits = bits;
                    string word=keyExpansionHelper.MakeWordFrom32Bits(bits);
                    roundWord.word=word;
                    keyWords.Add_RoundKeyWord(roundWord.word, roundWord.bits);
                }
            }
            keyWords=keyExpansionHelper.FillTheBytesInRoundKeyWord(keyWords);
            AES_Algorithm.Model.RoundKey roundKey = new AES_Algorithm.Model.RoundKey();
            roundKey=roundKey.MakeRoundKeysFromBytes(keyWords.BYTES);
            return (keyWords,roundKey);
        }
    }
}
