public class Add : Binop
{
   public Add(Expr l, Expr r) : base (l, r)
   {
      left = l;
      right = r;
   }

   public override string ToString() => $"({left} + {right})";

}