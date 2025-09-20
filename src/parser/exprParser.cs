namespace LuneDB
{
    public static class ExprParser
    {
        public static IExpr ParserIdendifierExpr(Parser p)
        {
            return new IdentifierExpr(p.advance().Value);
        }

        public static IExpr ParseExpr(Parser p, Lookup.binding_power bp)
        {
            Token.TokenType currentT = p.currentTokenType();

            if (!GlobalLookup.nud_lu.TryGetValue(currentT, out var handler) || handler == null)
            {
                throw new Exception($"NUD HANDLER EXPECTED FOR TOKEN: {currentT.ToString()}");
            }

            IExpr left = handler(p);

            while (true)
            {
                Token.TokenType nextTok = p.currentTokenType();

                if (!GlobalLookup.bp_lu.TryGetValue(nextTok, out var nextBp))
                {
                    nextBp = Lookup.binding_power.default_bp;
                }

                if ((int)nextBp <= (int)bp) break;

                if (!GlobalLookup.led_lu.TryGetValue(nextTok, out var ledHandler) || ledHandler == null)
                    throw new Exception($"LED HANDLER EXPECTED FOR TOKEN: {nextTok}");

                p.advance();

                left = ledHandler(p, left, nextBp);
            }

            return left;
        }
    }
}
