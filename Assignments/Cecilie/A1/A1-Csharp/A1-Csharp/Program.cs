

class Add : Binop
{
    protected override string Symbol => "+";
    public Add(Expr left, Expr right) : base(left, right) { }

    public override int Eval(Dictionary<string, int> env) => Left.Eval(env) + Right.Eval(env);
}

class Sub : Binop
{
    protected override string Symbol => "-";
    public Sub(Expr left, Expr right) : base(left, right) { }

    public override int Eval(Dictionary<string, int> env) => Left.Eval(env) - Right.Eval(env);
}

class Mul : Binop
{
    protected override string Symbol => "*";
    public Mul(Expr left, Expr right) : base(left, right) { }

    public override int Eval(Dictionary<string, int> env) => Left.Eval(env) * Right.Eval(env);
}

class Program
{
    static void Main()
    {
        Expr e = new Add(new CstI(17), new Var("z"));
        Expr e1 = new Sub(new Var("v"), new Add(new Var("w"), new Var("z")));
        Expr e2 = new Mul(new CstI(2), new Sub(new Var("v"), new Add(new Var("w"), new Var("z"))));
        Expr e3 = new Add(new Add(new Add(new Var("x"), new Var("y")), new Var("z")), new Var("v"));
        
        Console.WriteLine(e);
        Console.WriteLine(e1);
        Console.WriteLine(e2);
        Console.WriteLine(e3);
        
        var env = new Dictionary<string, int> {
            ["x"] = 5, ["y"] = 2, ["z"] = 10, ["v"] = 7, ["w"] = 1
        };

        Console.WriteLine($"e  = {e.Eval(env)}");   
        Console.WriteLine($"e1 = {e1.Eval(env)}");  
        Console.WriteLine($"e2 = {e2.Eval(env)}");  
        Console.WriteLine($"e3 = {e3.Eval(env)}");
    }
}