public class CstI : Expr {
    public int Value;
    public CstI(int v) { Value = v; }

    public override string ToString() {
        return Value.ToString();
    }

    public override int Eval(Dictionary<string, int> env) {
        return Value;
    }

    public override Expr Simplify() {
        return this; // Constants are already simplified
    }
}