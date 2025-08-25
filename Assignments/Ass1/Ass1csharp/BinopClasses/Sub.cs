public class Sub : Binop {
    public Sub(Expr l, Expr r) : base(l, r) { }
    public override string ToString() {
        return "(" + Left.ToString() + " - " + Right.ToString() + ")";
    }

    public override int Eval(Dictionary<string, int> env) {
        return Left.Eval(env) - Right.Eval(env);
    }
}
