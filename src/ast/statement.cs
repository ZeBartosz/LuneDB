namespace LuneDB
{
    public struct ExpressionStmt : IStmt
    {
        public IExpr expression;
        public ExpressionStmt(IExpr expression) => this.expression = expression;
        public void stmt() { }
    }

    public struct BlockStmt : IStmt
    {
        public List<IStmt> body;
        public BlockStmt(List<IStmt> body) => this.body = body;
        public void stmt() { }
    }
    public struct CreateStmt : IStmt
    {
        public Token.TokenType type;
        public IdentifierExpr name;
        public CreateStmt(Token.TokenType type, IdentifierExpr name)
        {
            this.type = type;
            this.name = name;
        }
        public void stmt() { }
    }
}
