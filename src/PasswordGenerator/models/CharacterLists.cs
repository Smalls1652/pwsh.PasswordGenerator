using System.Collections.Generic;

namespace PasswordGenerator.Models
{
    /// <summary>
    /// Houses lists of usable characters for password generation based on their character type.
    /// </summary>
    public class CharacterLists
    {
        public CharacterLists()
        {
            Initialize();
        }

        /// <summary>
        /// A list of alpha characters (A-Za-z).
        /// </summary>
        public List<CharacterItem> AlphaCharacters
        {
            get
            {
                return _alphaCharacters;
            }
        }
        private readonly List<CharacterItem> _alphaCharacters = new();

        /// <summary>
        /// A list of number characters (0-9).
        /// </summary>
        public List<CharacterItem> Numbers
        {
            get
            {
                return _numbers;
            }
        }
        private readonly List<CharacterItem> _numbers = new();

        /// <summary>
        /// A list of symbol characters.
        /// </summary>
        public List<CharacterItem> Symbols
        {
            get
            {
                return _symbols;
            }
        }
        private readonly List<CharacterItem> _symbols = new();

        /// <summary>
        /// A list of all characters in the lists for Alpha, Number, and Symbol characters.
        /// </summary>
        private readonly List<CharacterItem> allCharacterItems = new();

        /// <summary>
        /// Get any character in the 'allCharacterItems' list.
        /// </summary>
        /// <param name="utf32Code">The UTF32 decimal code for a character.</param>
        /// <returns></returns>
        public CharacterItem GetCharacter(int utf32Code)
        {
            return allCharacterItems.Find(
                (item) => item.Utf32Code == utf32Code
            );
        }

        /// <summary>
        /// Get a character in the 'AlphaCharacters' list.
        /// </summary>
        /// <param name="utf32Code">The UTF32 decimal code for a character.</param>
        /// <returns></returns>
        public CharacterItem GetAlphaCharacter(int utf32Code)
        {
            return AlphaCharacters.Find(
                (item) => item.Utf32Code == utf32Code
            );
        }

        /// <summary>
        /// Get a character in the 'Numbers' list.
        /// </summary>
        /// <param name="utf32Code">The UTF32 decimal code for a character.</param>
        /// <returns></returns>
        public CharacterItem GetNumberCharacter(int utf32Code)
        {
            return Numbers.Find(
                (item) => item.Utf32Code == utf32Code
            );
        }

        /// <summary>
        /// Get a character in the 'Symbols' list.
        /// </summary>
        /// <param name="utf32Code">The UTF32 decimal code for a character.</param>
        /// <returns></returns>
        public CharacterItem GetSymbolCharacter(int utf32Code)
        {
            return Symbols.Find(
                (item) => item.Utf32Code == utf32Code
            );
        }

        /// <summary>
        /// Initializes the class with all of the supported characters. Called from the default constructor.
        /// </summary>
        private void Initialize()
        {
            _alphaCharacters.AddRange(GetCharRange(65, 90));
            _alphaCharacters.AddRange(GetCharRange(97, 122));

            _numbers.AddRange(GetCharRange(48, 57));

            _symbols.AddRange(GetCharRange(33,47));
            _symbols.AddRange(GetCharRange(58,64));
            _symbols.AddRange(GetCharRange(91,96));
            _symbols.AddRange(GetCharRange(123,126));

            allCharacterItems.AddRange(AlphaCharacters);
            allCharacterItems.AddRange(Numbers);
            allCharacterItems.AddRange(Symbols);
        }

        /// <summary>
        /// Creates a list of <See cref="CharacterItem">CharacterItem</See> objects from a range numbers.
        /// </summary>
        /// <param name="startNum">The start number.</param>
        /// <param name="endNum">The end number.</param>
        /// <returns></returns>
        private static List<CharacterItem> GetCharRange(int startNum, int endNum)
        {
            List<CharacterItem> charList = new();

            for (int i = startNum; i <= endNum; i++)
                charList.Add(
                    new(i)
                );

            return charList;
        }
    }
}