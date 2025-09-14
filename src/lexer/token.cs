namespace LuneDB.Lexer
{
    public class Token
    {
        public enum TokenType
        {
            CREATE,
            TABLE,

            COLUMN,
            KEYWORD,
            IDENTIFIER,
            NUMBER,
            STRING,

            ERROR,

            // Brackets
            LEFT_PAREN,
            RIGHT_PAREN,
            LEFT_BRACE,
            RIGHT_BRACE,
            LEFT_BRACKET,
            RIGHT_BRACKET,

            // Misc
            WHITESPACE,
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
            return $"Token({Type}, \'{Value}\')";
        }

    }
}
