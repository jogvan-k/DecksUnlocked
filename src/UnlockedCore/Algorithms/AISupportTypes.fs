module UnlockedCore.Algorithms.AISupportTypes

open UnlockedCore


type nodeStates =
    | Node
    | Terminal
    | SearchLimit
    
let (|Node|Terminal|) (s: ICoreState) =
    let actions = s.Actions() |> Array.mapi (fun i a -> (a, i))
    if (actions.Length = 0)
    then Terminal 0
    else Node(actions |> Array.toSeq |> Seq.sortBy fst)

let (|SearchLimit|_|) (limit: searchLimit) (s: ICoreState) =
    match limit with
    | Turn (turn, _) when turn <= s.TurnNumber -> Some 0
    | Ply (remaining) when remaining <= 0 -> Some 0
    | _ -> None
