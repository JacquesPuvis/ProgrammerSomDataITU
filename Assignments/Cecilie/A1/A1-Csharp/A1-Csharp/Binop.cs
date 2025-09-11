namespace A1_Csharp;

public abstract class Binop : Expr
{
    protected Expr Left { get; }
    protected Expr Right { get; }
    protected abstract string Symbol { get; }
    
    protected Binop(Expr left, Expr right) { Left = left; Right = right; }
    
    public override string ToString() => $"({Left} {Symbol} {Right})";
}