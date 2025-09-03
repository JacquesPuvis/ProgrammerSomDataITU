public class Var : Expr
{
    string name;

    public Var(string v)
    {
        name = v;
    }
    public override string ToString() => name;

}