(* Programming language concepts for software developers, 2012-02-17 *)

(* Evaluation, checking, and compilation of object language expressions *)
(* Stack machines for expression evaluation                             *) 

(* Object language expressions with variable bindings and nested scope *)

module assignment1part2

type expr = 
  | CstI of int
  | Var of string
  | Let  of (string * expr) list * expr   // CHANGED: multiple sequential bindings
  | Prim of string * expr * expr;;

(* Some closed expressions: *)

let rec lookup env x =
    match env with 
    | []        -> failwith (x + " not found")
    | (y, v)::r -> if x=y then v else lookup r x;;

(* ---------------------------------------------------------------------- *)
    (* EXCERCISE 2.1*)

let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x 
    | Let (binds, ebody) ->
      let env' =
        List.fold (fun acc (x, erhs) ->
              let v = eval erhs acc   
              (x, v) :: acc) env binds
      eval ebody env'
    | Prim("+", e1, e2) -> eval e1 env + eval e2 env
    | Prim("*", e1, e2) -> eval e1 env * eval e2 env
    | Prim("-", e1, e2) -> eval e1 env - eval e2 env
    | Prim _            -> failwith "unknown primitive";;

let run e = eval e [];;

;

let rec mem x vs = 
    match vs with
    | []      -> false
    | v :: vr -> x=v || mem x vr;;



let rec union (xs, ys) = 
    match xs with 
    | []    -> ys
    | x::xr -> if mem x ys then union(xr, ys)
               else x :: union(xr, ys);;

(* minus xs ys  is the set of all elements in xs but not in ys *)

let rec minus (xs, ys) = 
    match xs with 
    | []    -> []
    | x::xr -> if mem x ys then minus(xr, ys)
               else x :: minus (xr, ys);;

(* ---------------------------------------------------------------------- *)
    (* EXCERCISE 2.2*)


let rec freevars e : string list =
    match e with
    | CstI i -> []
    | Var x  -> [x]
    | Let(binds, body) ->
      let (fvs, bound) =
        List.fold (fun (acc_fvs, acc_bound) (x, erhs) ->
                      // free vars of rhs, remove already bound ones
                      let fv_rhs = minus (freevars erhs, acc_bound)
                      // accumulate and extend bound set
                      (union(acc_fvs, fv_rhs), x::acc_bound))
                  ([], []) binds
      // body free vars minus all bound names
      let fv_body = minus (freevars body, bound)
      union(fvs, fv_body)
    | Prim(ope, e1, e2) -> union (freevars e1, freevars e2);;

(* Alternative definition of closed *)

let closed2 e = (freevars e = []);;


type texpr =                            (* target expressions *)
  | TCstI of int
  | TVar of int                         (* index into runtime environment *)
  | TLet of texpr * texpr               (* erhs and ebody                 *)
  | TPrim of string * texpr * texpr;;



let rec getindex vs x = 
    match vs with 
    | []    -> failwith "Variable not found"
    | y::yr -> if x=y then 0 else 1 + getindex yr x;;

(* ---------------------------------------------------------------------- *)
  (* EXCERCISE 2.3*)

let rec tcomp (e : expr) (cenv : string list) : texpr =
    match e with
    | CstI i -> TCstI i
    | Var x  -> TVar (getindex cenv x)
    | Let(binds, ebody) -> 
      match binds with
      | [] -> tcomp ebody cenv
      | (x, erhs) :: rest ->
        let ebody' = Let(rest, ebody)
        TLet(tcomp erhs cenv, tcomp ebody' (x :: cenv))
    | Prim(ope, e1, e2) -> TPrim(ope, tcomp e1 cenv, tcomp e2 cenv);;


let rec teval (e : texpr) (renv : int list) : int =
    match e with
    | TCstI i -> i
    | TVar n  -> List.nth renv n
    | TLet(erhs, ebody) -> 
      let xval = teval erhs renv
      let renv1 = xval :: renv 
      teval ebody renv1 
    | TPrim("+", e1, e2) -> teval e1 renv + teval e2 renv
    | TPrim("*", e1, e2) -> teval e1 renv * teval e2 renv
    | TPrim("-", e1, e2) -> teval e1 renv - teval e2 renv
    | TPrim _            -> failwith "unknown primitive";;

