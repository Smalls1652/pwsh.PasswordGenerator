using System.Collections.Generic;

namespace PasswordGenerator.Models
{
    public class CharacterLists
    {
        public CharacterLists()
        {
            Initialize();
        }

        public List<CharacterItem> AlphaCharacters
        {
            get
            {
                return _alphaCharacters;
            }
        }
        private readonly List<CharacterItem> _alphaCharacters = new();

        public List<CharacterItem> Numbers
        {
            get
            {
                return _numbers;
            }
        }
        private readonly List<CharacterItem> _numbers = new();

        public List<CharacterItem> Symbols
        {
            get
            {
                return _symbols;
            }
        }
        private readonly List<CharacterItem> _symbols = new();

        private readonly List<CharacterItem> allCharacterItems = new();

        public CharacterItem GetCharacter(int utf32Code)
        {
            return allCharacterItems.Find(
                (item) => item.Utf32Code == utf32Code
            );
        }

        public CharacterItem GetAlphaCharacter(int utf32Code)
        {
            return AlphaCharacters.Find(
                (item) => item.Utf32Code == utf32Code
            );
        }

        public CharacterItem GetNumberCharacter(int utf32Code)
        {
            return Numbers.Find(
                (item) => item.Utf32Code == utf32Code
            );
        }

        public CharacterItem GetSymbolCharacter(int utf32Code)
        {
            return Symbols.Find(
                (item) => item.Utf32Code == utf32Code
            );
        }

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