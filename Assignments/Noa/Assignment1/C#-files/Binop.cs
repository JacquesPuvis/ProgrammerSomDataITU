public abstract class Binop : Expr
{
    public Expr left, right;

    public Binop(Expr l, Expr r)
    {
        left = l;
        right = r;
    }
}