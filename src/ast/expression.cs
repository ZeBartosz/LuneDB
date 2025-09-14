namespace LuneDB
{

    // Literal Expressions
    public struct NumberExpr : Ast.IExpr
    {
        public double value;

        public NumberExpr(double value)
        {
            this.value = value;
        }

        public void expr() { }
    }

    public struct StringExpr : Ast.IExpr
    {
        public string value;

        public StringExpr(string value)
        {
            this.value = value;
        }

        public void expr() { }
    }

    // Complex Expressions
    public struct BinaryExpr : Ast.IExpr
    {
        public Ast.IExpr left;
        public Lexer.Token.TokenType token;
        public Ast.IExpr right;

        public BinaryExpr(Ast.IExpr left, Lexer.Token.TokenType token, Ast.IExpr right)
        {
            this.left = left;
            this.token = token;
            this.right = right;
        }

        public void expr() { }
    }
}
