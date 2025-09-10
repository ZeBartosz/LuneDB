using System.Text.RegularExpressions;

namespace LuneDB
{
    public class Lexer
    {
        public List<(Regex regex, Token.TokenType type)> patterns;
        public List<Token> tokens;
        public string input;
        public int position;

        public Lexer(string userInput)
        {
            input = userInput;
            position = 0;
            tokens = new List<Token>();
            patterns = new List<(Regex, Token.TokenType)>
            {
                (new Regex(@"\s+", RegexOptions.Compiled), Token.TokenType.WHITESPACE),
                (new Regex(@"\d+", RegexOptions.Compiled), Token.TokenType.NUMBER),
                (new Regex(@"[a-zA-Z_][a-zA-Z0-9_]*", RegexOptions.Compiled), Token.TokenType.IDENTIFIER),
            };
        }
        public void advance(int amount)
        {
            position += amount;
        }

        public void push(Token token)
        {
            this.tokens.Add(token);
        }

        public string at()
        {
            return this.input.Substring(position, 1);
        }

        public bool eof()
        {
            return position >= input.Length;
        }

        public string remainder()
        {
            return input.Substring(position);
        }

        public override string ToString()
        {
            return $"Lexer(input='{input}', position={position}, tokens={tokens})";
        }


        public Token.TokenType handleIndentifier(string value)
        {
            switch (value)
            {
                case "CREATE":
                    return Token.TokenType.CREATE;
                case "TABLE":
                    return Token.TokenType.TABLE;
                default:
                    return Token.TokenType.IDENTIFIER;
            }

        }

        public Lexer Tokenize()
        {
            Lexer lexer = this;

            while (!lexer.eof())
            {
                bool matched = false;

                foreach (var pattern in patterns)
                {
                    var match = pattern.regex.Match(this.input, position);

                    if (match.Success && match.Index == position)
                    {
                        if (pattern.type == Token.TokenType.WHITESPACE || match.Length == 0)
                        {
                            lexer.advance(match.Length);
                            continue;
                        }
                        matched = true;
                        lexer.advance(match.Length);

                        if (pattern.type == Token.TokenType.IDENTIFIER)
                        {
                            Token.TokenType type = handleIndentifier(match.Value);
                            lexer.push(new Token(type, match.Value));
                            break;
                        }

                        lexer.push(new Token(pattern.type, match.Value));
                        break;
                    }
                }
                if (!matched)
                {
                    lexer.advance(1);
                    lexer.push(new Token(Token.TokenType.ERROR, lexer.at().ToString()));
                }
            }

            lexer.push(new Token(Token.TokenType.EOF, "EOF"));
            return lexer;
        }
    }
}
