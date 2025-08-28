type expr =
    | CstI of int
    | Var of string
    | Let of string * expr * expr
    | Prim of string * expr * expr

let env = [("a", 3); ("c", 78); ("baf", 666); ("b", 111)];;

let rec lookup env x =
    match env with
        | [] -> failwith (x + " not found")
        | (y, v)::r -> if x=y then v else lookup r x;;

let rec eval e (env : (string * int) list) : int =
    match e with
        | CstI i -> i
        | Var x -> lookup env x
        | Let(x, erhs, ebody) ->
            let xval = eval erhs env
            let env1 = (x, xval) :: env
            eval ebody env1
        | Prim("+", e1, e2) -> eval e1 env + eval e2 env
        | Prim("*", e1, e2) -> eval e1 env * eval e2 env
        | Prim("-", e1, e2) -> eval e1 env - eval e2 env
        | Prim _ -> failwith "unknown primitive";;

let rec closedin (e : expr) (vs : string list) : bool =
    match e with
        | CstI i -> true
        | Var x -> List.exists (fun y -> x=y) vs
        | Let(x, erhs, ebody) ->
            let vs1 = x :: vs
            closedin erhs vs && closedin ebody vs1
        | Prim(ope, e1, e2) -> closedin e1 vs && closedin e2 vs

let vs = ["a"; "b"; "c"];;