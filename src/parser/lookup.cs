namespace LuneDB
{
    public class Lookup
    {
        public enum binding_power
        {
            default_bp = 0,
            comma = 1,           // Comma operator (e.g., function arguments)
            assignment = 2,      // Assignment operators (=, +=, etc.)
            logical = 3,         // Logical operators (&&, ||)
            relational = 4,      // Comparison operators (==, <, >, etc.)
            additive = 5,        // Addition and subtraction (+, -)
            multiplicative = 6,  // Multiplication, division, modulo (*, /, %)
            unary = 7,           // Unary operators (!, -)
            call = 8,            // Function calls (e.g., foo())
            member = 9,          // Object property access (e.g., obj.prop)
            primary = 10,
        }

        public Dictionary<Token.TokenType, Func<Parser, IExpr>> NudLookup { get; }
            = new Dictionary<Token.TokenType, Func<Parser, IExpr>>();
        public Dictionary<Token.TokenType, Func<Parser, IExpr, binding_power, IExpr>> LedLookup { get; }
            = new Dictionary<Token.TokenType, Func<Parser, IExpr, binding_power, IExpr>>();
        public Dictionary<Token.TokenType, Func<Parser, IStmt>> StmtLookup { get; }
            = new Dictionary<Token.TokenType, Func<Parser, IStmt>>();
        public Dictionary<Token.TokenType, binding_power> BpLookup { get; }
            = new Dictionary<Token.TokenType, binding_power>();

        public void RegisterNud(Token.TokenType type, Func<Parser, IExpr> handler)
            => NudLookup[type] = handler;
        public void RegisterLed(Token.TokenType type, Func<Parser, IExpr, binding_power, IExpr> handler)
            => LedLookup[type] = handler;
        public void RegisterStmt(Token.TokenType type, Func<Parser, IStmt> handler)
            => StmtLookup[type] = handler;
        public void RegisterBp(Token.TokenType type, binding_power bindingPower)
            => BpLookup[type] = bindingPower;
    }

    public class GlobalLookup
    {
        public static readonly Lookup LookupTable = new Lookup();

        public static Dictionary<Token.TokenType, Lookup.binding_power>
            bp_lu => LookupTable.BpLookup;
        public static Dictionary<Token.TokenType, Func<Parser, IExpr>>
            nud_lu => LookupTable.NudLookup;
        public static Dictionary<Token.TokenType, Func<Parser, IExpr, Lookup.binding_power, IExpr>>
      led_lu => LookupTable.LedLookup;
        public static Dictionary<Token.TokenType, Func<Parser, IStmt>>
            stmt_lu => LookupTable.StmtLookup;

        public static void createLookup()
        {
            LookupTable.RegisterNud(Token.TokenType.IDENTIFIER, ExprParser.ParserIdendifierExpr);
        }
    }

}
