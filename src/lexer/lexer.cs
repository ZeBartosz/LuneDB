using System.Text.RegularExpressions;

namespace LuneDB
{
    public class Lexer
    {
        private readonly string _input;
        private readonly List<Token> _tokens = new();
        private int _position = 0;
        private static readonly List<(Regex regex, Token.TokenType type)> s_patterns =
        new List<(Regex, Token.TokenType)>
                    {
                        (new Regex(@"\s+", RegexOptions.Compiled), Token.TokenType.WHITESPACE),
                        (new Regex(@"\d+", RegexOptions.Compiled), Token.TokenType.NUMBER),
                        (new Regex(@"[a-zA-Z_][a-zA-Z0-9_]*", RegexOptions.Compiled), Token.TokenType.IDENTIFIER),
                        (new Regex(@"\;", RegexOptions.Compiled), Token.TokenType.SEMICOLUMN),
                        (new Regex(@"\(", RegexOptions.Compiled), Token.TokenType.LEFT_PAREN),
                        (new Regex(@"\)", RegexOptions.Compiled), Token.TokenType.RIGHT_PAREN),
                        (new Regex(@"\{", RegexOptions.Compiled), Token.TokenType.LEFT_BRACE),
                        (new Regex(@"\}", RegexOptions.Compiled), Token.TokenType.RIGHT_BRACE),
                        (new Regex(@"\[", RegexOptions.Compiled), Token.TokenType.LEFT_BRACKET),
                        (new Regex(@"\]", RegexOptions.Compiled), Token.TokenType.RIGHT_BRACKET),
                    };

        public Lexer(string userInput) => _input = userInput;
        public IReadOnlyList<Token> Tokens => _tokens.AsReadOnly();

        public IReadOnlyList<Token> Tokenize()
        {
            while (!Eof())
            {
                bool matched = false;

                foreach (var pattern in s_patterns)
                {
                    var match = pattern.regex.Match(_input, _position);

                    if (match.Success && match.Index == _position)
                    {
                        if (pattern.type == Token.TokenType.WHITESPACE || match.Length == 0)
                        {
                            Advance(match.Length);
                            continue;
                        }
                        matched = true;
                        Advance(match.Length);

                        if (pattern.type == Token.TokenType.IDENTIFIER)
                        {
                            Token.TokenType type = HandleIdentifier(match.Value);
                            Push(new Token(type, match.Value));
                            break;
                        }

                        Push(new Token(pattern.type, match.Value));
                        break;
                    }
                }
                if (!matched)
                {
                    throw new Exception($"LEXER_ERROR: Token at position \' {_position} \'  \'{At().ToString()}\' unrecognised");
                }
            }

            Push(new Token(Token.TokenType.EOF, "EOF"));

            return Tokens;
        }

        // Helpers
        private void Advance(int amount) => _position += amount;
        private void Push(Token token) => _tokens.Add(token);
        private string At() => _input.Substring(_position, 1);
        private bool Eof() => _position >= _input.Length;
        private string Remainder() => _input.Substring(_position);

        public override string ToString() => $"Lexer(input='{_input}', position={_position}, tokens={_tokens})";

        Token.TokenType HandleIdentifier(string value)
        {
            switch (value)
            {
                case "CREATE": return Token.TokenType.CREATE;
                case "DATABASE": return Token.TokenType.DATABASE;
                case "TABLE": return Token.TokenType.TABLE;
                default: return Token.TokenType.IDENTIFIER;
            }

        }

    }
}
