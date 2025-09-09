using System.Text.RegularExpressions;

namespace LuneDB
{
    public class Lexer
    {
        public List<Regex> patterns;
        public List<Token> tokens;
        public string input;
        public int position;

        public Lexer(string userInput)
        {
            input = userInput;
            position = 0;
            tokens = new List<Token>();
            patterns = new List<Regex>
            {
                new Regex(@"\s+", RegexOptions.Compiled),
                new Regex(@"\d+", RegexOptions.Compiled),
                new Regex(@"[a-zA-Z_][a-zA-Z0-9_]*", RegexOptions.Compiled),
                new Regex(@"==|!=|<=|>=|<|>", RegexOptions.Compiled),
                new Regex(@"\+|-|\*|/", RegexOptions.Compiled),
                new Regex(@"\(|\)|\[|\]|\{|\}", RegexOptions.Compiled),
                new Regex(@";", RegexOptions.Compiled)
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

        public Lexer Tokenize()
        {
            Lexer lexer = this;

            while (!lexer.eof())
            {
                bool matched = false;

                foreach (Regex pattern in patterns)
                {
                    Match match = pattern.Match(this.input, position);
                    if (match.Success && match.Index == position)
                    {
                        if (match.Value.Equals(" ") || match.Length == 0)
                        {
                            lexer.advance(match.Length);
                            continue;
                        }
                        matched = true;
                        lexer.advance(match.Length);

                        lexer.push(new Token(Token.TokenType.IDENTIFIER, match.Value));
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
