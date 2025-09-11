namespace A1_Csharp.Binops;

public class Add : Binop
{
    protected override string Symbol => "+";
    public Add(Expr left, Expr right) : base(left, right) { }

    public override int Eval(Dictionary<string, int> env) => Left.Eval(env) + Right.Eval(env);
}