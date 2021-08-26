namespace PasswordGenerator.Models
{
    public class CharacterItem
    {
        public CharacterItem(int utf32Code)
        {
            Utf32Code = utf32Code;
        }

        public int Utf32Code { get; }

        public string Character
        {
            get
            {
                return char.ConvertFromUtf32(Utf32Code);
            }
        }

        public override string ToString()
        {
            return Character;
        }
    }
}