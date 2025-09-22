namespace LuneDB
{
    public class Lookup
    {
        public enum BindingPower
        {
            DEFAULT = 0,
            COMMA = 1,           // Comma operator (e.g., function arguments)
            ASSIGNMENT = 2,      // Assignment operators (=, +=, etc.)
            LOGICAL = 3,         // Logical operators (&&, ||)
            RELATIONAL = 4,      // Comparison operators (==, <, >, etc.)
            ADDITIVE = 5,        // Addition and subtraction (+, -)
            MULTIPLICATIVE = 6,  // Multiplication, division, modulo (*, /, %)
            UNARY = 7,           // Unary operators (!, -)
            CALL = 8,            // Function calls (e.g., foo())
            MEMBER = 9,          // Object property access (e.g., obj.prop)
            PRIMARY = 10,
        }

        public Dictionary<Token.TokenType, Func<Parser, IExpr>> NudLookup { get; }
            = new Dictionary<Token.TokenType, Func<Parser, IExpr>>();
        public Dictionary<Token.TokenType, Func<Parser, IExpr, BindingPower, IExpr>> LedLookup { get; }
            = new Dictionary<Token.TokenType, Func<Parser, IExpr, BindingPower, IExpr>>();
        public Dictionary<Token.TokenType, Func<Parser, IStmt>> StmtLookup { get; }
            = new Dictionary<Token.TokenType, Func<Parser, IStmt>>();
        public Dictionary<Token.TokenType, BindingPower> BpLookup { get; }
            = new Dictionary<Token.TokenType, BindingPower>();

        public void RegisterNud(Token.TokenType type, Func<Parser, IExpr> handler)
            => NudLookup[type] = handler;
        public void RegisterLed(Token.TokenType type, Func<Parser, IExpr, BindingPower, IExpr> handler)
            => LedLookup[type] = handler;
        public void RegisterStmt(Token.TokenType type, Func<Parser, IStmt> handler)
            => StmtLookup[type] = handler;
        public void RegisterBp(Token.TokenType type, BindingPower bindingPower)
            => BpLookup[type] = bindingPower;
    }

    public class GlobalLookup
    {
        public static readonly Lookup LookupTable = new Lookup();

        public static Dictionary<Token.TokenType, Lookup.BindingPower>
            bp_lu => LookupTable.BpLookup;
        public static Dictionary<Token.TokenType, Func<Parser, IExpr>>
            nud_lu => LookupTable.NudLookup;
        public static Dictionary<Token.TokenType, Func<Parser, IExpr, Lookup.BindingPower, IExpr>>
            led_lu => LookupTable.LedLookup;
        public static Dictionary<Token.TokenType, Func<Parser, IStmt>>
            stmt_lu => LookupTable.StmtLookup;

        public static void CreateLookup()
        {
            LookupTable.RegisterNud(Token.TokenType.IDENTIFIER, ExprParser.ParserIdendifier);
            LookupTable.RegisterStmt(Token.TokenType.CREATE, StmtParser.ParseCreate);
        }
    }

}
