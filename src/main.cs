
namespace LuneDB
{
    public class Program
    {

        public static void Main(string[] args)
        {
            string userinput = "this my name is";

            Lexer tokens = new Lexer(userinput).Tokenize();
            IStmt ast = new Parser(tokens.tokens).Parse();
            PrintParser.PrintStmt(ast);
        }
    }
}
