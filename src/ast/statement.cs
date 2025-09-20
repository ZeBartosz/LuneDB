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
}
