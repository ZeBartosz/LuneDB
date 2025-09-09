namespace LuneDB
{
    public class Token
    {
        public enum TokenType
        {
            TABLE,
            COLUMN,
            KEYWORD,
            IDENTIFIER,
            NUMBER,
            STRING,

            ERROR,
            EOF,
        }

        public TokenType Type { get; set; }
        public string Value { get; set; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"Token({Type}, {Value})";
        }

    }
}
