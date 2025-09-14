namespace LuneDB
{

    // Literal Expressions
    public struct NumberExpr : IExpr
    {
        public double value;

        public NumberExpr(double value)
        {
            this.value = value;
        }

        public void expr() { }
    }

    public struct StringExpr : IExpr
    {
        public string value;

        public StringExpr(string value)
        {
            this.value = value;
        }

        public void expr() { }
    }

    // Complex Expressions
    public struct BinaryExpr : IExpr
    {
        public IExpr left;
        public Token.TokenType token;
        public IExpr right;

        public BinaryExpr(IExpr left, Token.TokenType token, IExpr right)
        {
            this.left = left;
            this.token = token;
            this.right = right;
        }

        public void expr() { }
    }
}
