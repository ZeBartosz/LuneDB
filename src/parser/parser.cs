namespace LuneDB
{
    public class Parser
    {
        private IReadOnlyList<Token> _tokens { get; }
        private int pos = 0;

        public Parser(IReadOnlyList<Token> tokens)
        {
            _tokens = tokens;
            this.pos = 0;
        }

        // Helpers
        public Token currentToken()
        {
            if (pos >= _tokens.Count)
            {
                return new Token(Token.TokenType.EOF, "eof");
            }
            return _tokens[pos];

        }

        public Token.TokenType currentTokenType()
        {
            return currentToken().Type;
        }

        public Token advance()
        {
            if (pos < _tokens.Count) { pos++; }


            return currentToken();
        }

        public bool hasTokens()
        {
            return pos < _tokens.Count && currentTokenType() != Token.TokenType.EOF;
        }

        public Token expectError(Token.TokenType expectedToken, string errorMsg)
        {
            Token currentToken = this.currentToken();

            if (currentToken.Type != expectedToken)
            {
                throw new Exception($"expected \"{expectedToken}\", but received \"{currentToken.Type}\"");
            }

            return advance();
        }

        public IStmt Parse()
        {
            GlobalLookup.createLookup();
            Parser parser = new Parser(_tokens);
            StmtParser stmtParser = new StmtParser(parser);
            List<IStmt> body = new List<IStmt>();

            while (parser.hasTokens())
            {
                IStmt stmt = stmtParser.ParseStmt();
                body.Add(stmt);
            }

            return new BlockStmt(body);
        }

    }

}
