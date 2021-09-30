using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
        public static SecureString CreatePassword_SecureString(int passwordLength, List<char> ignoreChars, bool shuffle = false)
        {
            // Create a randomized password char array.
            char[] charList = CreatePasswordCharArray(passwordLength, ignoreChars, shuffle);

            // Initialize the SecureString and append all chars.
            SecureString secureString = new();
            foreach (char item in charList)
                secureString.AppendChar(item);

            // Set the secure string to read-only, since we're through with creating it.
            secureString.MakeReadOnly();

            return secureString;
        }

        /// <summary>
        /// Generates a randomly generated password and returns as a string.
        /// </summary>
        /// <param name="passwordLength">The length the password should be.</param>
        /// <param name="ignoreChars">An array of characters to be ignored from the generated password.</param>
        /// <returns></returns>
        public static string CreatePassword_String(int passwordLength, List<char> ignoreChars, bool shuffle = false)
        {
            // Create a randomized password char array.
            char[] charList = CreatePasswordCharArray(passwordLength, ignoreChars, shuffle);

            // Initialize the StringBuilder and append all chars to it.
            StringBuilder stringBuilder = new();
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
        private static char[] CreatePasswordCharArray(int passwordLength, List<char> ignoreChars, bool shuffle)
        {
            // Instantiate a char array of the provided password length.
            char[] randomChars = new char[passwordLength];

            List<Task<char>> charGenTasks = new();

            // Fill the char array with random characters.
            for (int i = 0; i <= (passwordLength - 1); i++)
            {
                charGenTasks.Add(
                    Task.Run(
                        () =>
                        {
                            char generatedChar = new();
                            bool charGenerated = false;
                            while (!charGenerated)
                            {
                                // Generate the random character.
                                generatedChar = GetRandomCharacter();

                                // Check to make sure the generated character is not in the ignore list if provided.
                                if ((ignoreChars != null) && (ignoreChars.Contains(generatedChar) == true))
                                {
                                    charGenerated = false;
                                }
                                else
                                {
                                    // Set the index of the char array to the generated character.
                                    // Then exit the while loop to move to the next index.
                                    // randomChars[i] = generatedChar;

                                    charGenerated = true;
                                }
                            }

                            return generatedChar;
                        }
                    )
                );
            }

            Task.WaitAll(charGenTasks.ToArray());
            for (int i = 0; i <= (passwordLength - 1); i++)
            {
                randomChars[i] = charGenTasks[i].Result;
                charGenTasks[i].Dispose();
            }

            if (shuffle)
            {
                // Start shuffling the char array.
                // Instantiate an integer list for storing already shuffled indexes.
                List<int> shuffledIndexes = new();

                // Instantiate a new char array that matches the size as the original char array.
                char[] randomCharsShuffled = new char[randomChars.Length];

                // Iterate through each index of the original char array.
                for (int i = 0; i <= (randomChars.Length - 1); i++)
                {
                    bool indexCompleted = false;
                    while (!indexCompleted)
                    {
                        // Generate a random number between 0 and the length of the original char array (minus 1).
                        int randomIndex = GetRandomNumber(0, (randomChars.Length - 1));

                        // Check to ensure that the random index number hasn't already been used.
                        if ((shuffledIndexes.Contains(randomIndex) == false))
                        {
                            // Add the index to the integer list so it's not processed again.
                            // Then add the character to the new char array.
                            shuffledIndexes.Add(randomIndex);
                            randomCharsShuffled[i] = randomChars[randomIndex];

                            // Exit the while loop and continue to the next index.
                            indexCompleted = true;
                        }
                    }
                }

                // Clear the integer list and free the memory.
                shuffledIndexes.Clear();
                shuffledIndexes.Capacity = 0;

                randomChars = randomCharsShuffled;
            }

            return randomChars;
        }

        /// <summary>
        /// Randomly select a single character.
        /// </summary>
        /// <returns></returns>
        public static char GetRandomCharacter()
        {
            // Get the type of character to randomly generate.
            CharacterType charType = GetRandomCharacterType();
            CharacterItem charItem;

            // Get a random character based off of the type.
            switch (charType)
            {
                // Type = Number
                case CharacterType.NumberCharacter:
                    charItem = characterLists.Numbers[GetRandomNumber(0, (characterLists.Numbers.ToArray().Length - 1))];
                    break;

                // Type = Symbol
                case CharacterType.SymbolCharacter:
                    charItem = characterLists.Symbols[GetRandomNumber(0, (characterLists.Symbols.ToArray().Length - 1))];
                    break;

                // Type = Alpha
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
        /// <param name="maxValue">The maximum value that can be returned. Defaults to '100'.</param>
        /// <returns></returns>
        public static int GetRandomNumber(int minValue = 0, int maxValue = 100)
        {
            // Create a byte array that can hold 4 bytes.
            // The reason why it's 4 bytes is because:
            // 4 bytes == A 32-bit integer
            byte[] randomNumBytes = new byte[4];

            // Use the 'RandomNumberGenerator' class to cryptographically generate random bytes.
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                // Fill the byte array with random bytes.
                rng.GetBytes(randomNumBytes);
            }

            // Get the absolute value of the data in the byte array as an integer.
            int generatedNum = Math.Abs(
                BitConverter.ToInt32(randomNumBytes, 0)
            );

            // Calculate the difference between the max value (plus 1) and the minimum value.
            int minMaxDiff = (maxValue + 1) - minValue;

            // Get the random number by adding the minimum value to the modulus of the random byte array data and the min/max difference. 
            int randomNum = minValue + (generatedNum % minMaxDiff);
            return randomNum;
        }
    }
}
