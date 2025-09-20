namespace LuneDB

{
    public class StmtParser
    {
        private readonly Parser _parser;
        public StmtParser(Parser parser) => _parser = parser;

        public IStmt ParseStmt()
        {
            Token.TokenType currentT = _parser.currentTokenType();

            if (GlobalLookup.stmt_lu.TryGetValue(currentT, out var handler))
            {
                return handler(_parser);
            }

            IStmt expression = ParseExprStmt();

            return expression;
        }

        public IStmt ParseExprStmt()
        {
            IExpr expression = ExprParser.ParseExpr(_parser, Lookup.binding_power.default_bp);

            return new ExpressionStmt(expression);
        }
    }
}
