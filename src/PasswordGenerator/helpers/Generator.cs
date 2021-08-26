using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace PasswordGenerator.Helpers
{
    using Models;

    public static class Generator
    {
        public readonly static CharacterLists characterLists = new();

        public static string CreatePassword(int passwordLength, List<string> ignoreChars)
        {
            List<string> randomChars = new();

            for (int i = 1; i <= passwordLength; i++)
            {
                bool charGenerated = false;
                while (!charGenerated)
                {
                    string generatedChar = GetRandomCharacter();

                    if ((ignoreChars != null) && (ignoreChars.Contains(generatedChar) == true))
                    {
                        charGenerated = false;
                    }
                    else
                    {
                        randomChars.Add(generatedChar);
                        charGenerated = true;
                    }
                }
            }

            return string.Join("", randomChars);
        }

        public static string GetRandomCharacter()
        {
            CharacterType charType = GetRandomCharacterType();
            CharacterItem charItem;

            switch (charType)
            {
                case CharacterType.NumberCharacter:
                    charItem = characterLists.Numbers[GetRandomNumber(0, (characterLists.Numbers.ToArray().Length - 1))];
                    break;

                case CharacterType.SymbolCharacter:
                    charItem = characterLists.Symbols[GetRandomNumber(0, (characterLists.Symbols.ToArray().Length - 1))];
                    break;

                default:
                    charItem = characterLists.AlphaCharacters[GetRandomNumber(0, (characterLists.AlphaCharacters.ToArray().Length - 1))];
                    break;
            }

            return charItem.Character;
        }

        public static CharacterType GetRandomCharacterType()
        {
            return (CharacterType)GetRandomNumber(0, 2);
        }

        public static int GetRandomNumber(int minValue, int maxValue)
        {
            int randomNum;
            byte[] randomNumBytes = new byte[4];
            int minMaxDiff = (maxValue + 1) - minValue;

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumBytes);
            }

            int generatedNum = Math.Abs(
                BitConverter.ToInt32(randomNumBytes, 0)
            );

            randomNum = minValue + (generatedNum % minMaxDiff);

            return randomNum;
        }
    }
}
