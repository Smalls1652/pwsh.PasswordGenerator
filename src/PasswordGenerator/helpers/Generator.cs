using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace PasswordGenerator.Helpers
{
    using Models;

    /// <summary>
    /// This is the core generator for generating random passwords.
    /// </summary>
    public static class Generator
    {
        public readonly static CharacterLists characterLists = new();

        /// <summary>
        /// Generates a randomized password.
        /// </summary>
        /// <param name="passwordLength">The length the password should be.</param>
        /// <param name="ignoreChars">An array of characters to be ignored from the generated password.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Randomly select a single character.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get a random character type. Used for randomly selecting what type of character to generate.
        /// </summary>
        /// <returns></returns>
        private static CharacterType GetRandomCharacterType()
        {
            return (CharacterType)GetRandomNumber(0, 2);
        }

        /// <summary>
        /// Generate a random number within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum value that can be returned. Defaults to '0'.</param>
        /// <param name="maxValue">The maximum value that can be returned. Defaults to '100'</param>
        /// <returns></returns>
        public static int GetRandomNumber(int minValue = 0, int maxValue = 100)
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
