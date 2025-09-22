namespace LuneDB
{
    public static class ExprParser
    {
        public static IExpr ParserIdendifier(Parser p)
            => new IdentifierExpr(p.Consume().Value);

        public static IExpr ParseExpr(Parser p, Lookup.BindingPower bp)
        {
            Token.TokenType currentT = p.PeekType();

            if (!GlobalLookup.nud_lu.TryGetValue(currentT, out var handler) || handler == null)
                throw new ParserException($"NUD HANDLER EXPECTED FOR TOKEN: {currentT.ToString()}");

            IExpr left = handler(p);

            while (true)
            {
                Token.TokenType nextTok = p.PeekType();

                if (!GlobalLookup.bp_lu.TryGetValue(nextTok, out var nextBp))
                    nextBp = Lookup.BindingPower.DEFAULT;

                if ((int)nextBp <= (int)bp) break;

                if (!GlobalLookup.led_lu.TryGetValue(nextTok, out var ledHandler) || ledHandler == null)
                    throw new ParserException($"LED HANDLER EXPECTED FOR TOKEN: {nextTok}");

                p.Consume();

                left = ledHandler(p, left, nextBp);
            }

            return left;
        }


    }
}
