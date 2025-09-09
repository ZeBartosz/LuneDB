namespace LuneDB
{
    public class Token
    {
        public enum TokenType
        {
            ADD,
            SUBTRACT,
            MULTIPLY,
            DIVIDE,
            LEFT_PAREN,
            RIGHT_PAREN,
            NUMBER,
            STRING,
            IDENTIFIER,
            EOF
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
