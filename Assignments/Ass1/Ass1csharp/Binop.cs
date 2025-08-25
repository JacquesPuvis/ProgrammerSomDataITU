public abstract class Binop : Expr {
    public Expr Left, Right;
    public Binop(Expr l, Expr r) { Left = l; Right = r; }
}