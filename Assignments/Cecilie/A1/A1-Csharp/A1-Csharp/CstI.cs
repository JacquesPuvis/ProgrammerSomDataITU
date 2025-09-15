namespace A1_Csharp;

public class CstI : Expr
{
    public int Value { get; }
    
    public CstI(int value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public override Expr Simplify()
    {
        return this;
    }

    public override int Eval(Dictionary<string, int> env)
    {
        return Value;
    }
}