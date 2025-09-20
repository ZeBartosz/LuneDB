
namespace LuneDB.Tests
{
    public class LexerTests
    {
        [Fact]
        public void Tokenize_Identifiers()
        {
            var input = "CREATE tableName _id1 abc123";
            var lexer = new Lexer(input);
            var tokens = lexer.Tokenize()
                              .Where(t => t.Type != Token.TokenType.WHITESPACE && t.Type != Token.TokenType.EOF)
                              .ToList();

            Assert.Equal(Token.TokenType.CREATE, tokens[0].Type);
            Assert.Equal("tableName", tokens[1].Value);
        }
    }
}
