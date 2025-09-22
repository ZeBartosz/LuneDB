
namespace LuneDB
{
    public static class StmtParser
    {
        public static IStmt ParseStmt(Parser p)
        {
            Token.TokenType currentT = p.PeekType();

            if (GlobalLookup.stmt_lu.TryGetValue(currentT, out var handler))
                return handler(p);

            return ParseExpr(p);
        }

        public static IStmt ParseExpr(Parser p)
        {
            IExpr expression = ExprParser.ParseExpr(p, Lookup.BindingPower.DEFAULT);

            return new ExpressionStmt(expression);
        }

        public static IStmt ParseCreate(Parser p)
        {
            Token.TokenType createType = Token.TokenType.DATABASE;
            IdentifierExpr name = new IdentifierExpr(" DefaultVarable  ");

            p.Consume();

            if (p.PeekType() == Token.TokenType.DATABASE)
            {
                createType = p.Consume().Type;
                name = new IdentifierExpr(p.ExpectToken(Token.TokenType.IDENTIFIER).Value);

            }

            p.ExpectToken(Token.TokenType.SEMICOLUMN);

            return new CreateStmt(createType, name);
        }
    }
}
