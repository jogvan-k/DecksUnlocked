module UnlockedCore.Algorithms.AISupportTypes

open UnlockedCore

type nodeStates =
    | Node
    | Terminal
    | SearchLimit

type remainingSearch =
    | Plies of plies: int
    | Turns of turns: int * plyLimit: int

let (|Node|Terminal|) (s: ICoreState * int list) =
    let actions = (fst s).Actions() |> Seq.mapi (fun i a -> (a, i)) 
    if (Seq.isEmpty actions)
    then Terminal 0
    else match (snd s) with
         | [] -> Node(actions |> Seq.sortBy fst |> Seq.map (fun i -> (fst i, snd i, [])))
         | head :: tail ->
             let firstAction = actions |> Seq.find (fun a -> (snd a) = head) |> Seq.singleton |> Seq.map (fun a -> (fst a, snd a, tail))
             let restActions = actions |> Seq.filter (fun a -> (snd a) <> head) |> Seq.sortBy fst |> Seq.map (fun a -> (fst a, snd a, []))
             Node(Seq.append firstAction  restActions )

let (|SearchLimit|_|) (limit: remainingSearch) (s: ICoreState * int list) =
    match limit with
    | Turns (turn, _) when turn <= (fst s).TurnNumber -> Some 0
    | Plies (remaining) when remaining <= 0 -> Some 0
    | _ -> None
