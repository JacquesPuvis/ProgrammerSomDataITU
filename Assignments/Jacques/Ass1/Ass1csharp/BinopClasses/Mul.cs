public class Mul : Binop {
    public Mul(Expr l, Expr r) : base(l, r) { }
    public override string ToString() {
        return "(" + Left.ToString() + " * " + Right.ToString() + ")";
    }

    public override int Eval(Dictionary<string, int> env) {
        return Left.Eval(env) * Right.Eval(env);
    }

    public override Expr Simplify() {
        Expr leftSimple = Left.Simplify();
        Expr rightSimple = Right.Simplify();
        
        // 0 * e = 0
        if (leftSimple is CstI leftConst && leftConst.Value == 0) {
            return new CstI(0);
        }
        
        // e * 0 = 0
        if (rightSimple is CstI rightConst && rightConst.Value == 0) {
            return new CstI(0);
        }
        
        // 1 * e = e
        if (leftSimple is CstI leftOne && leftOne.Value == 1) {
            return rightSimple;
        }
        
        // e * 1 = e
        if (rightSimple is CstI rightOne && rightOne.Value == 1) {
            return leftSimple;
        }
        
        // c1 * c2 = (c1*c2) for constants
        if (leftSimple is CstI leftC && rightSimple is CstI rightC) {
            return new CstI(leftC.Value * rightC.Value);
        }
        
        return new Mul(leftSimple, rightSimple);
    }
}