// Create an environment (dictionary) for variable values
var env = new Dictionary<string, int> {
    {"z", 5},
    {"a", 3},
    {"b", 7}
};

Binop e = new Add(new CstI(17), new Var("z"));
Binop f = new Mul(new CstI(2), new CstI(2));
Binop g = new Add(new CstI(1), new CstI(2));

Console.WriteLine("Expression: " + e);
Console.WriteLine("Evaluation: " + e.Eval(env));
Console.WriteLine("Simplified: " + e.Simplify());
Console.WriteLine();

Console.WriteLine("Expression: " + f);
Console.WriteLine("Evaluation: " + f.Eval(env));
Console.WriteLine("Simplified: " + f.Simplify());
Console.WriteLine();

Console.WriteLine("Expression: " + g);
Console.WriteLine("Evaluation: " + g.Eval(env));
Console.WriteLine("Simplified: " + g.Simplify());
Console.WriteLine();

// More complex example
Expr complex = new Mul(new Var("a"), new Add(new Var("b"), new CstI(3)));
Console.WriteLine("Expression: " + complex);
Console.WriteLine("Evaluation: " + complex.Eval(env));
Console.WriteLine("Simplified: " + complex.Simplify());
Console.WriteLine();

// Examples that show simplification rules
Console.WriteLine("=== Simplification Examples ===");
Expr addZero = new Add(new Var("x"), new CstI(0));
Console.WriteLine("x + 0 = " + addZero.Simplify());

Expr mulOne = new Mul(new CstI(1), new Var("y"));
Console.WriteLine("1 * y = " + mulOne.Simplify());

Expr mulZero = new Mul(new Var("x"), new CstI(0));
Console.WriteLine("x * 0 = " + mulZero.Simplify());

Expr subZero = new Sub(new Var("x"), new CstI(0));
Console.WriteLine("x - 0 = " + subZero.Simplify());

// Nested simplification
Expr nested = new Add(new Mul(new CstI(0), new Var("x")), new Mul(new CstI(1), new Var("y")));
Console.WriteLine("(0 * x) + (1 * y) = " + nested.Simplify());