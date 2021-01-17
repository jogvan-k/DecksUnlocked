module UnlockedCore.MCTS.Algorithm

open UnlockedCore
open UnlockedCore.MCTS.Types

let isUnexplored l =
    match l with
    | Unexplored _ -> true
    | _ -> false
    
let isLeaf l =
    match l with
    | Leaf _ -> true
    | _ -> false

let leafEvaluator (l: Leaf) =
    match l with
    | Terminal win -> if(win) then 1. else 0.
    | Unexplored _ -> 1.
    | Leaf s -> 0.5

let rec selection (s: State, leafEvaluator) =
    if (Array.isEmpty s.leaves) then
        SelectionResult.Exhausted Array.empty
    else
    match s.leaves |> Array.indexed |>  Array.maxBy (fun i -> leafEvaluator(snd i)) with
    | (i, Terminal(win)) -> SelectionResult.Exhausted(Array.singleton i)
    | (i, Unexplored(a)) -> SelectionResult.Candidate(s, i)
    | (i, Leaf(s)) -> selection(s, leafEvaluator)
    

let search(s: ICoreState) =
    let root = State(Parent.None, s)
    
    for i in 1..1000 do
        selection(root, leafEvaluator) |> ignore
    
    Array.empty