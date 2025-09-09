
namespace LuneDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // string? TokenType = Console.ReadLine();
            // string? TokenValue = Console.ReadLine();

            Token token = new Token(Token.TokenType.ADD, "+");

            Console.WriteLine(token.ToString());

        }
    }
}
