// 1) x - (3 + y)

using System;

Expr e1 = new Sub(
    new Var("x"),
    new Add(new CstI(3), new Var("y"))
);

// 2) 2 * (10 - z)
Expr e2 = new Mul(
    new CstI(2),
    new Sub(new CstI(10), new Var("z"))
);

// 3) (x * x) + (3 * x)     // i.e., x^2 + 3x
Expr e3 = new Add(
    new Mul(new Var("x"), new Var("x")),
    new Mul(new CstI(3), new Var("x"))
);

Console.WriteLine(e1);
Console.WriteLine(e2);
Console.WriteLine(e3);