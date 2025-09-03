(* Programming language concepts for software developers, 2010-08-28 *)

(* Evaluating simple expressions with variables *)

module Intro2

(* Association lists map object language variables to their values *)

// Exercise 1.1
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
  | Prim of string * expr * expr;;

let e1 = CstI 17;;

let e2 = Prim("+", CstI 3, Var "a");;

let e3 = Prim("+", Prim("*", Var "b", CstI 9), Var "a");;


(* Evaluation within an environment *)

let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x 
    | Prim("+", e1, e2) -> eval e1 env + eval e2 env
    | Prim("*", e1, e2) -> eval e1 env * eval e2 env
    | Prim("-", e1, e2) -> eval e1 env - eval e2 env
    | Prim _            -> failwith "unknown primitive";;

let e1v  = eval e1 env;;
let e2v1 = eval e2 env;;
let e2v2 = eval e2 [("a", 314)];;
let e3v  = eval e3 env;;

// Exercise 1.2
type aexpr = 
  | CstI of int
  | Var of string
  | Add of aexpr * aexpr
  | Mul of aexpr * aexpr
  | Sub of aexpr * aexpr;;
  
let v, w, z, x, y = CstI(12), CstI(12), CstI(30), CstI(22), CstI(8)

let a1 = Sub(v, Add(w, z))
let a2 = Mul(CstI(2), a1)
let a3 = Add(x, Add(y, Add(z, v)))
  
let rec fmt (a : aexpr) : string =
    match a with
    | CstI(x) -> string x
    | Var(x) -> x
    | Add(x, y) -> "(" +  fmt x + " + " + fmt y + ")"
    | Mul(x, y) -> "(" + fmt x + " * " + fmt y + ")"
    | Sub(x, y) -> "(" + fmt x + " - " + fmt y + ")"
  
let rec simplify  (a : aexpr) : aexpr =
    match a with
    | CstI(x) -> CstI(x)
    | Var(x) -> Var(x)
    | Add(x, y) ->
        let x1 = simplify x
        let y2 = simplify y
        match x1, y2 with
        | CstI(0), a -> a
        | a, CstI(0) -> a
        | a, b -> Add(a, b)
    | Mul(x, y) ->
        let x1 = simplify x
        let y2 = simplify y
        match x1, y2 with
        | CstI(1), a -> a
        | a, CstI(1) -> a
        | CstI(0), a -> a
        | a, CstI(0) -> a
        | a, b -> Mul(a, b)
    | Sub(x, y) ->
        let x1 = simplify x
        let y2 = simplify y
        match x1, y2 with
        | a, b when a = b -> CstI(0)
        | a, b -> Sub(a, b)
     

  
  
  
  
  
  
  
  
