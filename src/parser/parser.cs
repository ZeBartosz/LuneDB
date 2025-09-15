namespace LuneDB
{
    public class Parser
    {
        public List<Token> tokens { get; }
        public int pos { get; set; }

        Parser(List<Token> tokens)
        {
            this.tokens = tokens;
            this.pos = 0;
        }

        // Helpers
        public Token currentToken()
        {
            if (pos >= tokens.Count)
            {
                return new Token(Token.TokenType.EOF, "eof");
            }
            return tokens[pos];

        }

        public Token.TokenType currentTokenType()
        {
            return currentToken().Type;
        }

        public Token advance()
        {
            if (pos < tokens.Count) { pos++; }


            return currentToken();
        }

        public bool hasTokens()
        {
            return pos < tokens.Count && currentTokenType() == Token.TokenType.EOF;
        }

        public Token expectError(Token.TokenType expectedToken, string errorMsg)
        {
            Token currentToken = this.currentToken();

            if (currentToken.Type != expectedToken)
            {
                new Exception($"expected \"{expectedToken}\", but received \"{currentToken.Type}\"");
            }

            return advance();
        }

        public IStmt parse(List<Token> tokens)
        {
            Parser parser = new Parser(tokens);
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
