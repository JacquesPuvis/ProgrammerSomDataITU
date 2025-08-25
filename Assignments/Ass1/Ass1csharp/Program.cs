// Create an environment (dictionary) for variable values
var env = new Dictionary<string, int> {
    {"z", 5},
    {"a", 3},
    {"b", 7}
};

Expr e = new Add(new CstI(17), new Var("z"));
Expr f = new Mul(new CstI(2), new CstI(2));
Binop g = new Add(new CstI(1), new CstI(2));

Console.WriteLine("Expression: " + e);
Console.WriteLine("Evaluation: " + e.Eval(env));
Console.WriteLine();

Console.WriteLine("Expression: " + f);
Console.WriteLine("Evaluation: " + f.Eval(env));
Console.WriteLine();

Console.WriteLine("Expression: " + g);
Console.WriteLine("Evaluation: " + g.Eval(env));
Console.WriteLine();

// More complex example
Expr complex = new Mul(new Var("a"), new Add(new Var("b"), new CstI(3)));
Console.WriteLine("Expression: " + complex);
Console.WriteLine("Evaluation: " + complex.Eval(env));