public class Sub : Binop
{
    Expr left, right;
    public Sub(Expr l, Expr r) : base (l, r)
    {
        left = l;
        right = r;
    }
    public override string ToString() => $"({left} - {right})";

}