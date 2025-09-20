namespace LuneDB
{
    public static class PrintParser
    {

        static string Indent(int level) => new string(' ', level * 2);

        public static void PrintStmt(IStmt stmt, int indent = 0)
        {
            switch (stmt)
            {
                case ExpressionStmt es:
                    Console.WriteLine($"{Indent(indent)}ExpressionStmt");
                    PrintExpr(es.expression, indent + 1);
                    break;

                case BlockStmt bs:
                    Console.WriteLine($"{Indent(indent)}BlockStmt");
                    foreach (var s in bs.body)
                        PrintStmt(s, indent + 1);
                    break;
                default:
                    Console.WriteLine($"{Indent(indent)}UnknownStmt: {stmt.GetType().Name}");
                    break;
            }
        }

        static void PrintExpr(IExpr expr, int indent = 0)
        {
            switch (expr)
            {
                case IdentifierExpr id:
                    Console.WriteLine($"{Indent(indent)}Identifier: {id.value}");
                    break;
                case BinaryExpr bin:
                    Console.WriteLine($"{Indent(indent)}BinaryExpr ({bin.token})");
                    PrintExpr(bin.left, indent + 1);
                    PrintExpr(bin.right, indent + 1);
                    break;
                default:
                    Console.WriteLine($"{Indent(indent)}UnknownExpr: {expr.GetType().Name}");
                    break;
            }
        }
    }
}
