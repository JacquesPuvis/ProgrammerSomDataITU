public class Mul : Binop
{
    Expr left, right;

    public Mul(Expr l, Expr r) : base (l, r)
    {
        left = l;
        right = r;
    }

    public override string ToString() => $"({left} * {right})";

}