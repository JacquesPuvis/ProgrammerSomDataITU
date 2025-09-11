(*SCROLL TO BOTTOM FOR QUESTIONS *)

module Intcomp1

type expr = 
  | CstI of int
  | Var of string
  | Let of string * expr * expr
  | Prim of string * expr * expr;;

let rec getindex vs x = 
    match vs with 
    | []    -> failwith "Variable not found"
    | y::yr -> if x=y then 0 else 1 + getindex yr x;;


type rinstr =
  | RCstI of int
  | RAdd 
  | RSub
  | RMul 
  | RDup
  | RSwap;;


let rec rcomp (e : expr) : rinstr list =
    match e with
    | CstI i            -> [RCstI i]
    | Var _             -> failwith "rcomp cannot compile Var"
    | Let _             -> failwith "rcomp cannot compile Let"
    | Prim("+", e1, e2) -> rcomp e1 @ rcomp e2 @ [RAdd]
    | Prim("*", e1, e2) -> rcomp e1 @ rcomp e2 @ [RMul]
    | Prim("-", e1, e2) -> rcomp e1 @ rcomp e2 @ [RSub]
    | Prim _            -> failwith "unknown primitive";;
            


type sinstr =
  | SCstI of int                        (* push integer           *)
  | SVar of int                         (* push variable from env *)
  | SAdd                                (* pop args, push sum     *)
  | SSub                                (* pop args, push diff.   *)
  | SMul                                (* pop args, push product *)
  | SPop                                (* pop value/unbind var   *)
  | SSwap;;                             (* exchange top and next  *)
 


type stackvalue =
  | Value                               (* A computed value *)
  | Bound of string;;                   (* A bound variable *)


let rec scomp (e : expr) (cenv : stackvalue list) : sinstr list =
    match e with
    | CstI i -> [SCstI i]
    | Var x  -> [SVar (getindex cenv (Bound x))]
    | Let(x, erhs, ebody) -> 
          scomp erhs cenv @ scomp ebody (Bound x :: cenv) @ [SSwap; SPop]
    | Prim("+", e1, e2) -> 
          scomp e1 cenv @ scomp e2 (Value :: cenv) @ [SAdd] 
    | Prim("-", e1, e2) -> 
          scomp e1 cenv @ scomp e2 (Value :: cenv) @ [SSub] 
    | Prim("*", e1, e2) -> 
          scomp e1 cenv @ scomp e2 (Value :: cenv) @ [SMul] 
    | Prim _ -> failwith "scomp: unknown operator";;

(* Output the integers in list inss to the text file called fname: *)

let intsToFile (inss : int list) (fname : string) = 
    let text = String.concat " " (List.map string inss)
    System.IO.File.WriteAllText(fname, text);;

(* -----------------------------------------------------------------  *)
let testSinsr: sinstr list = [SCstI 8; SCstI 10; SAdd]
let testExpr = Let("z",CstI 17, Prim("+",Var "z", Var "z"));;


(*____________________________________________________________________________________________________________________________*)
(*2.4 FROM THE BOOK*)
//The PDF was a bit contradictory to what was written in the book, so we made the assignments from the book


let scst  = 0
let svar  = 1
let sadd  = 2
let ssub  = 3
let smul  = 4
let spop  = 5
let sswap = 6



let rec assemble (sl : sinstr list) : int list =
  match sl with
  | [] -> []
  | head :: slx ->
      match head with
      | SCstI n -> scst :: n :: assemble slx
      | SVar v -> svar :: v :: assemble slx
      | SAdd   -> sadd :: assemble slx
      | SSub   -> ssub :: assemble slx
      | SMul   -> smul :: assemble slx
      | SPop   -> spop :: assemble slx
      | SSwap  -> sswap :: assemble slx


(*2.5 FROM THE BOOK*)
let assembleToFile (code : sinstr list) (outfile : string) : unit =
    let bytes = assemble code
    intsToFile bytes outfile


let exprToBytecode (e : expr) : int list =
    assemble (scomp e [])

