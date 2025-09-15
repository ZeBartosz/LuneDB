namespace LuneDB
{
    public struct BlockStmt : IStmt
    {
        public List<IStmt> body;

        public BlockStmt(List<IStmt> body)
        {
            this.body = body;
        }

        public void stmt() { }
    }
}
