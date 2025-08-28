public class Add : Binop {
    public Add(Expr l, Expr r) : base(l, r) { }
    public override string ToString() {
        return "(" + Left.ToString() + " + " + Right.ToString() + ")";
    }

    public override int Eval(Dictionary<string, int> env) {
        return Left.Eval(env) + Right.Eval(env);
    }

    public override Expr Simplify() {
        Expr leftSimple = Left.Simplify();
        Expr rightSimple = Right.Simplify();
        
        // 0 + e = e
        if (leftSimple is CstI leftConst && leftConst.Value == 0) {
            return rightSimple;
        }
        
        // e + 0 = e
        if (rightSimple is CstI rightConst && rightConst.Value == 0) {
            return leftSimple;
        }
        
        // c1 + c2 = (c1+c2) for constants
        if (leftSimple is CstI leftC && rightSimple is CstI rightC) {
            return new CstI(leftC.Value + rightC.Value);
        }
        
        return new Add(leftSimple, rightSimple);
    }
}
