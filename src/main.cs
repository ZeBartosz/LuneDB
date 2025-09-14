
namespace LuneDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string userinput = "this () {} [ is a TABLE test ] CREATE  1231 312 qqq";

            Lexer.Lexer tokens = new Lexer.Lexer(userinput).Tokenize();

            foreach (Lexer.Token token in tokens.tokens)
            {
                Console.WriteLine(token.ToString());
            }
        }
    }
}
