public class Var : Expr {
    public string Name;
    public Var(string n) { Name = n; }

    public override string ToString() {
        return Name;
    }

    public override int Eval(Dictionary<string, int> env) {
        if (env.ContainsKey(Name)) {
            return env[Name];
        } else {
            throw new ArgumentException($"Variable '{Name}' not found in environment");
        }
    }
}