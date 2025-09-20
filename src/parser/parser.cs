namespace LuneDB
{
    public class Parser
    {
        private IReadOnlyList<Token> _tokens { get; }
        private int _pos = 0;

        public Parser(IReadOnlyList<Token> tokens) => _tokens = tokens;

        // Helpers
        public bool HasTokens() => _pos < _tokens.Count && CurrentTokenType() != Token.TokenType.EOF;
        public Token.TokenType CurrentTokenType() => CurrentToken().Type;

        public Token CurrentToken()
        {
            if (_pos >= _tokens.Count) return new Token(Token.TokenType.EOF, "eof");

            return _tokens[_pos];
        }

        public Token Advance()
        {
            if (_pos < _tokens.Count) _pos++;

            return CurrentToken();
        }


        public Token ExpectError(Token.TokenType expectedToken, string errorMsg)
        {
            Token currentToken = CurrentToken();

            if (currentToken.Type != expectedToken)
                throw new Exception($"expected \"{expectedToken}\", but received \"{currentToken.Type}\"");

            return Advance();
        }

        public IStmt Parse()
        {
            GlobalLookup.createLookup();

            Parser parser = new Parser(_tokens);
            List<IStmt> body = new List<IStmt>();

            while (parser.HasTokens())
            {
                IStmt stmt = StmtParser.ParseStmt(parser);
                body.Add(stmt);
            }

            return new BlockStmt(body);
        }
    }
}
