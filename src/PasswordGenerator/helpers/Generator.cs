using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Text;

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
        /// Generates a randomly generated password and returns as a SecureString.
        /// </summary>
        /// <param name="passwordLength">The length the password should be.</param>
        /// <param name="ignoreChars">An array of characters to be ignored from the generated password.</param>
        /// <returns></returns>
        public static SecureString CreatePassword_SecureString(int passwordLength, List<char> ignoreChars)
        {
            SecureString secureString = new();
            char[] charList = CreatePasswordCharArray(passwordLength, ignoreChars);

            foreach (char item in charList)
                secureString.AppendChar(item);

            secureString.MakeReadOnly();

            return secureString;
        }

        /// <summary>
        /// Generates a randomly generated password and returns as a string.
        /// </summary>
        /// <param name="passwordLength">The length the password should be.</param>
        /// <param name="ignoreChars">An array of characters to be ignored from the generated password.</param>
        /// <returns></returns>
        public static string CreatePassword_String(int passwordLength, List<char> ignoreChars)
        {
            StringBuilder stringBuilder = new();
            char[] charList = CreatePasswordCharArray(passwordLength, ignoreChars);

            foreach (char item in charList)
                stringBuilder.Append(item);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Generates a randomized password into a char array.
        /// </summary>
        /// <param name="passwordLength">The length the password should be.</param>
        /// <param name="ignoreChars">An array of characters to be ignored from the generated password.</param>
        /// <returns></returns>
        private static char[] CreatePasswordCharArray(int passwordLength, List<char> ignoreChars)
        {
            char[] randomChars = new char[passwordLength];

            for (int i = 0; i <= (passwordLength - 1); i++)
            {
                bool charGenerated = false;
                while (!charGenerated)
                {
                    char generatedChar = GetRandomCharacter();

                    if ((ignoreChars != null) && (ignoreChars.Contains(generatedChar) == true))
                    {
                        charGenerated = false;
                    }
                    else
                    {
                        randomChars[i] = generatedChar;
                        charGenerated = true;
                    }
                }
            }

            List<int> shuffledIndexes = new();
            char[] randomCharsShuffled = new char[randomChars.Length];
            for (int i = 0; i <= (randomChars.Length - 1); i++)
            {
                bool indexCompleted = false;
                while (!indexCompleted)
                {
                    int randomIndex = GetRandomNumber(0, (randomChars.Length - 1));
                    if ((shuffledIndexes.Contains(randomIndex) == false))
                    {
                        shuffledIndexes.Add(randomIndex);
                        randomCharsShuffled[i] = randomChars[randomIndex];

                        indexCompleted = true;
                    }
                }
            }

            shuffledIndexes.Clear();

            return randomCharsShuffled;
        }

        /// <summary>
        /// Randomly select a single character.
        /// </summary>
        /// <returns></returns>
        public static char GetRandomCharacter()
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
