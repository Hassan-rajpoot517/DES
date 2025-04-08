using DES_Algo.AES_Algorithm.Model;
using DES_Algo.DES_Algorithm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES_Algo.AES_Algorithm
{
    public class AES
    {
        public (Model.Round ,RoundWord,Model.RoundKey,RoundKeyWord) AES_128(string plaintext,string key)
        {
            Model.Round round = new Model.Round();
            RoundWord roundWords = new RoundWord();
            RoundHelper roundHelper = new RoundHelper();
            RoundKeyGenerator roundKeyGenerator = new RoundKeyGenerator();
            (RoundKeyWord roundKeyWord, Model.RoundKey roundKey) RoundKeys=roundKeyGenerator.GenerateKeys(key);
            RoundWord roundWord = new RoundWord();
            roundWord=roundHelper.Convert_Plainbits_to_StateMatrix(roundHelper.ConvertKeyTextIntoBitsArray(plaintext));
            //InitialTransformation
            roundWord=roundHelper.AddRoundKey(roundWord, RoundKeys.roundKeyWord, 0);
            for (int i = 0;i<roundWord.roundwords.Count ;i++) 
            { 
                roundWords.Add_RoundWord(roundWord.getter(i).word,roundWord.getter(i).wordBits);
            }
            int startingIndex = 4;
            for (int i = 1; i <= 10; i++)
            {
                if (i == 10)
                {
                    roundWord = roundHelper.SubstiteBytes(roundWord);
                    roundWord = roundHelper.ShiftRows(roundWord);
                    roundWord = roundHelper.AddRoundKey(roundWord, RoundKeys.roundKeyWord, startingIndex);
                    for (int k = 0; k < roundWord.roundwords.Count; k++)
                    {
                        roundWords.Add_RoundWord(roundWord.getter(k).word, roundWord.getter(k).wordBits);
                    }
                    startingIndex += 4;
                }
                else
                {
                    roundWord = roundHelper.SubstiteBytes(roundWord);
                    roundWord = roundHelper.ShiftRows(roundWord);
                    roundWord = roundHelper.MixColumn(roundWord);
                    roundWord = roundHelper.AddRoundKey(roundWord, RoundKeys.roundKeyWord, startingIndex);
                    for (int k = 0; k < roundWord.roundwords.Count; k++)
                    {
                        roundWords.Add_RoundWord(roundWord.getter(k).word, roundWord.getter(k).wordBits);
                    }
                    startingIndex += 4;
                }
            }
            roundWords =roundHelper.FillTheBytesInRoundWord(roundWords);
            round=round.MakeRoundsFromBytes(roundWords.wordBYTES);
            return (round, roundWords,RoundKeys.roundKey,RoundKeys.roundKeyWord);
        }
    }
}
