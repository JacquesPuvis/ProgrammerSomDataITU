namespace A1_Csharp;

public class Var : Expr
{
    public string Name { get; }
    
    public Var(string name)
    {
        Name = name;
    }
    
    public override string ToString()
    {
        return Name;
    }

    public override Expr Simplify()
    {
        return this;
    }

    public override int Eval(Dictionary<string, int> env)
    {
        return env.TryGetValue(Name, out var v) 
            ? v : throw new KeyNotFoundException($"Variable '{Name}' not found");
    } 
}