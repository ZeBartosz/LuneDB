namespace LuneDB
{
    public class ParserException : Exception
    {
        public ParserException(string message) : base(message) { }
    }

    public class Parser
    {
        private readonly IReadOnlyList<Token> _tokens;
        private int _position = 0;

        public Parser(IReadOnlyList<Token> tokens) => _tokens = tokens ?? Array.Empty<Token>();

        public IStmt Parse()
        {
            GlobalLookup.CreateLookup();

            List<IStmt> body = new List<IStmt>();

            while (HasMoreTokens())
                body.Add(StmtParser.ParseStmt(this));

            return new BlockStmt(body);
        }

        // Helpers
        public bool HasMoreTokens() => _position < _tokens.Count && PeekType() != Token.TokenType.EOF;
        public Token.TokenType PeekType() => Peek().Type;

        public Token Peek()
        {
            if (_position >= _tokens.Count) return new Token(Token.TokenType.EOF, "EOF");

            return _tokens[_position];
        }

        public Token Consume()
        {
            Token previousToken = Peek();
            if (_position < _tokens.Count) _position++;

            return previousToken;
        }

        public Token ExpectToken(Token.TokenType expectedToken, string? errorMessage = null)
        {
            Token currentToken = Peek();

            if (currentToken.Type != expectedToken)
            {
                string error = errorMessage ?? $"EXPECTATION ERROR: Expected \"{expectedToken}\", but received \"{currentToken.Type}\" at position {_position}";

                throw new ParserException(error);
            }

            return Consume();
        }
    }
}
