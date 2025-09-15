
namespace LuneDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string userinput = "this";

            Lexer tokens = new Lexer(userinput).Tokenize();

            foreach (Token token in tokens.tokens)
            {
                Console.WriteLine(token.ToString());
            }
        }
    }
}
