namespace LuneDB
{

    // Literal Expressions
    public struct NumberExpr : ast.IExpr
    {
        public double value;

        public NumberExpr(double value)
        {
            this.value = value;
        }

        public void expr() { }
    }

    public struct StringExpr : ast.IExpr
    {
        public string value;

        public StringExpr(string value)
        {
            this.value = value;
        }

        public void expr() { }
    }

    // Complex Expressions
    public struct BinaryExpr : ast.IExpr
    {
        public ast.IExpr left;
        public Lexer token;
        public ast.IExpr right;

        public BinaryExpr(ast.IExpr left, Lexer token, ast.IExpr right)
        {
            this.left = left;
            this.token = token;
            this.right = right;
        }

        public void expr() { }
    }
}
