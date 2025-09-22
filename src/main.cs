namespace LuneDB
{
    public class Program
    {

        public static void Main(string[] args)
        {
            string userinput = "CREATE DATABASE db;";

            IReadOnlyList<Token> tokens = new Lexer(userinput).Tokenize();
            foreach (Token token in tokens) Console.WriteLine(token.ToString());

            IStmt ast = new Parser(tokens).Parse();
            PrintParser.PrintStmt(ast);
        }
    }
}
