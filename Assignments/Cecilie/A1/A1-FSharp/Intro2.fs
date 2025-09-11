(* Programming language concepts for software developers, 2010-08-28 *)

(* Evaluating simple expressions with variables *)

module Intro2

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
  | If of expr * expr * expr

let e1 = CstI 17;;

let e2 = Prim("+", CstI 3, Var "a");;

let e3 = Prim("+", Prim("*", Var "b", CstI 9), Var "a");;

let e4 = Prim("+", Prim("max", Var "b", CstI 112), Var "a");;

let e5 = Prim("==", Prim("min", Var "b", CstI 112), CstI 111);;


(* Evaluation within an environment *)

let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x 
    | Prim(ope, e1, e2) ->
        let i1 = eval e1 env
        let i2 = eval e2 env
        match ope with
        |"+" -> i1 + i2
        |"*" -> i1 * i2
        |"-" -> i1 - i2
        |"max" -> if i1 > i2 then i1 else i2
        |"min" -> if i1 < i2 then i1 else i2
        |"==" -> if i1 = i2 then 1 else 0
        |_ -> failwith "unknown primitive"
    | If(e1, e2, e3) ->
        let i1 = eval e1 env
        if i1 <> 0 then eval e2 env else eval e3 env

let e1v  = eval e1 env;;
let e2v1 = eval e2 env;;
let e2v2 = eval e2 [("a", 314)];;
let e3v  = eval e3 env;;

type aexpr =
    | CstI of int
    | Var of string
    | Add of aexpr * aexpr
    | Mul of aexpr * aexpr
    | Sub of aexpr * aexpr

let ae1 = Sub(Var "a", Add(CstI 3, CstI 7))
let ae2 = Mul(CstI 2, ae1)
let ae3 = Add(CstI 1, Add(CstI 2, Add(CstI 3, CstI 4)))

let rec fmt ae : string =
    match ae with
    | CstI i -> string i
    | Var x -> x
    | Add(ae1, ae2) -> "(" + fmt ae1 + " + " + fmt ae2 + ")"
    | Mul(ae1, ae2) -> "(" + fmt ae1 + " * " + fmt ae2 + ")"
    | Sub(ae1, ae2) -> "(" + fmt ae1 + " - " + fmt ae2 + ")"
    
let rec simplify aexpr : aexpr =
    match aexpr with
    | CstI i -> CstI i
    | Var x -> Var x
    | Add(ae1, ae2) ->
        let i1 = simplify ae1
        let i2 = simplify ae2
        match i1, i2 with
        | CstI 0, e | e, CstI 0 -> e
        | CstI a, CstI b -> CstI(a + b)
        | _ -> Add(i1, i2)
    | Mul(ae1, ae2) ->
        let i1, i2 = simplify ae1, simplify ae2
        match i1, i2 with
        | CstI 0, _ | _, CstI 0 -> CstI 0
        | CstI 1, e | e, CstI 1 -> e
        | CstI a, CstI b -> CstI(a * b)
        | _ -> Mul(i1, i2)
    | Sub(ae1, ae2) ->
        let i1, i2 = simplify ae1, simplify ae2
        match i1, i2 with
        | e, CstI 0 -> e
        | _ when i1 = i2 -> CstI 0
        | CstI a, CstI b -> CstI(a - b)
        | _ -> Sub(i1, i2)


let rec diff ae x : aexpr =
    match ae with
    | CstI _ -> CstI 0
    | Var y -> if y = x then CstI 1 else CstI 0
    | Add(ae1, ae2) -> Add(diff ae1 x, diff ae2 x)
    | Sub(ae1, ae2) -> Sub(diff ae1 x, diff ae2 x)
    | Mul(ae1, ae2) -> Add(Mul(diff ae1 x, ae2), Mul(ae1, diff ae2 x))