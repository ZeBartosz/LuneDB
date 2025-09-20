namespace LuneDB
{
    public static class StmtParser
    {
        public static IStmt ParseStmt(Parser p)
        {
            Token.TokenType currentT = p.PeekType();

            if (GlobalLookup.stmt_lu.TryGetValue(currentT, out var handler))
                return handler(p);

            return ParseExprStmt(p);
        }

        public static IStmt ParseExprStmt(Parser p)
        {
            IExpr expression = ExprParser.ParseExpr(p, Lookup.BindingPower.DEFAULT);

            return new ExpressionStmt(expression);
        }
    }
}
