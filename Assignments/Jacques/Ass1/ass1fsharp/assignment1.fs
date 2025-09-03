(* Programming language concepts for software developers, 2010-08-28 *)

(* Evaluating simple expressions with variables *)

module assignment1

(* Association lists map object language variables to their values *)

let env = [("a", 3); ("c", 78); ("baf", 666); ("b", 111)];;

let emptyenv = []; (* the empty environment *)

let rec lookup env x =
    match env with 
    | []        -> failwith (x + " not found")
    | (y, v)::r -> if x=y then v else lookup r x;;

let cvalue = lookup env "c";;


(* Object language expressions with variables *)

type expr = 
  | CstI of int
  | Var of string
  | Prim of string * expr * expr
  | If of expr * expr * expr;;


let e1 = CstI 17;;

let e2 = Prim("+", CstI 3, Var "a");;

let e3 = Prim("+", Prim("*", Var "b", CstI 9), Var "a");;

let e4 = Prim ("==", CstI 17, e1);;

let e5 = Prim ("max", e1, e2);;
let e6 = Prim ("min", e1, e2);;
let e7 = If(Var "a", CstI 11, CstI 22)


(* Evaluation within an environment *)
//(i)
let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x 
    | Prim("+", e1, e2) -> eval e1 env + eval e2 env
    | Prim("*", e1, e2) -> eval e1 env * eval e2 env
    | Prim("-", e1, e2) -> eval e1 env - eval e2 env
    | Prim ("==", e1,e2) -> if eval e1 env = eval e2 env then 1 else 0
    | Prim ("max", e1,e2) -> if eval e1 env < eval e2 env then eval e2 env else eval e1 env
    | Prim ("min", e1,e2) -> if eval e1 env < eval e2 env then eval e1 env else eval e2 env
    //(iv) (v)
    | If(e1, e2, e3)    ->
        let cond = eval e1 env
        if cond <> 0 then eval e2 env else eval e3 env
    | _ -> failwith "unknown expression"




//(iii)
let rec eval2 e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x 
    | Prim(ope, e1, e2) -> 
        let i1 = eval2 e1 env
        let i2 = eval e2 env
        match ope with
        | "+" -> i1 + i2
        | "*" -> i1 * i2
        | "-" -> i1 - i2
        | "==" -> if i1 = i2 then 1 else 0
        | "max" -> if i1 < i2 then i2 else i1
        | "min" -> if i1 < i2 then i1 else i2
        | _ -> failwith (ope + " unknown")
    | _ -> failwith "unknown expression"

let e1v  = eval e1 env;;
let e2v1 = eval e2 env;;
let e2v2 = eval e2 [("a", 314)];;
let e3v  = eval e3 env;;

//(ii)
let e4v  = eval e4 env;;
let e5v  = eval e5 env;;
let e6v  = eval e6 env;;

let e7v = eval e7 env



// (1.2)
// (i)
type aexpr =
  | CstI of int
  | Var of string
  | Add of aexpr * aexpr
  | Mul of aexpr * aexpr
  | Sub of aexpr * aexpr

let rec aeval (ae : aexpr) (env : (string * int) list) : int =
    match ae with
    | CstI i -> i
    | Var x -> lookup env x
    | Add (a,b) -> aeval a env + aeval b env 
    | Mul (a,b) -> aeval a env * aeval b env
    | Sub (a,b) -> aeval a env - aeval b env

let ae1 = Mul(Var "a", Add (Var "c", CstI 3))

//(ii)
let ae2 = Sub(Var "v", Add(Var "w",Var "z"))
let ae3 = Mul(CstI 2, Sub(Var "v", Add(Var "w", Var "z")))

let ae4 = Add(Var "x", Add(Var "y", Add(Var "z", Var "v")))


//(iii)

let rec fmt (ae: aexpr) : string =
    match ae with
    | CstI i        -> string i
    | Var x         -> x
    | Add (a, b)  -> "(" + fmt a + " + " + fmt b + ")"
    | Mul (a, b)  -> "(" + fmt a + " * " + fmt b + ")"
    | Sub (a, b)  -> "(" + fmt a + " - " + fmt b + ")"


let ae5 = Add(Var "x", Add(Var "y", Mul(Var "z", CstI 1)))


let rec simplify (ae1: aexpr) : aexpr =
    match ae1 with
    | CstI i -> CstI i
    | Var x -> Var x
    | Add (a, b) ->
        let sa = simplify a
        let sb = simplify b
        match (sa, sb) with
        | (CstI 0, _) -> sb
        | (_, CstI 0) -> sa
        | _ -> Add (sa, sb)
    | Mul (a, b) ->
        let sa = simplify a
        let sb = simplify b
        match (sa, sb) with
        | (CstI 0, _) -> CstI 0
        | (_, CstI 0) -> CstI 0
        | (CstI 1, _) -> sb
        | (_, CstI 1) -> sa
        | _ -> Mul (sa, sb)
    | Sub (a, b) ->
        let sa = simplify a
        let sb = simplify b
        match (sa, sb) with
        | (_, CstI 0) -> sa
        | _ -> Sub (sa, sb)







        