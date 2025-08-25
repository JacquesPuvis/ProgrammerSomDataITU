public class CstI : Expr {
    public int Value;
    public CstI(int v) { Value = v; }

    public override string ToString() {
        return Value.ToString();
    }

    public override int Eval(Dictionary<string, int> env) {
        return Value;
    }
}