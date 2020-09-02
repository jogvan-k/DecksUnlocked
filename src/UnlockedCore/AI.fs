module AIMethods

open System
open UnlockedCore

type searchLimit =
    | Until of turn: int
    | Depth of remaining: int

let reduce =
    function
    | Until turn -> Until turn
    | Depth remaining -> Depth(remaining - 1)

type nodeStates =
    | Node
    | Terminal
    | SearchLimit

let (|Node|Terminal|) (s: ICoreState) =
    let actions = s.Actions()
    if (actions.Length = 0)
    then Terminal 0
    else Node(Array.map (fun (a: ICoreAction) -> a.DoCoreAction()) actions)

let (|SearchLimit|_|) (limit: searchLimit) (s: ICoreState) =
    match limit with
    | Until (turn) when turn <= s.TurnNumber -> Some 0
    | Depth (remaining) when remaining <= 0 -> Some 0
    | _ -> None


let rec minimaxAI evaluator (depth: searchLimit) (s: ICoreState) =
    match s with
    | SearchLimit depth ex
    | Terminal ex -> (evaluator s, [])
    | Node a ->
        let evaluatedStates = evaluate a evaluator depth
        if (s.PlayerTurn = Player.Player1)
        then evaluatedStates |> Array.maxBy (fun r -> fst r)
        else evaluatedStates |> Array.minBy (fun r -> fst r)

and evaluate a evaluator depth =
    Array.mapi (fun i s ->
        let eval = (minimaxAI evaluator (reduce depth) s)
        (fst eval, i :: (snd eval))) a

let randomMoveAI (rng: Random) (s: ICoreState) =
    let actions = s.Actions()
    rng.Next() % actions.Length
