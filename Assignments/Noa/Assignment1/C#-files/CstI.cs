public class CstI : Expr
{
    private int i;

    public CstI(int v)
    {
        i = v;
    }
    public override string ToString() => i.ToString();

}