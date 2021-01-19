module UnlockedCore.MCTS.Algorithm

open System
open System.Diagnostics
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
let winRate (actingPlayer: Player, s: State) = if(actingPlayer = Player.Player1) then s.winRate else 1. - s.winRate
let explorationConstant = sqrt(2.)
let explorationRate (s: State) =
    match s.parent with
    | None -> 0.
    | Parent p -> explorationConstant * sqrt(log(float(p.visitCount)) / float(s.visitCount))

let leafEvaluator (l: Leaf, actingPlayer: Player) =
    match l with
    | Terminal win -> if(win && actingPlayer = Player.Player1 || (not win && actingPlayer = Player.Player2)) then 0. else -1.
    | Unexplored _ -> 1.
    | Leaf s -> Math.Min(1., (winRate(actingPlayer, s) + explorationRate s))

let rec selection (s: State, leafEvaluator) =
    if (Array.isEmpty s.leaves) then Exhausted s
    else
    match s.leaves |> Array.indexed |>  Array.maxBy (fun i -> leafEvaluator(snd i, s.state.PlayerTurn)) with
    | (_, Terminal _) -> Exhausted(s)
    | (i, Unexplored _) -> Candidate(s, i)
    | (_, Leaf s) -> selection(s, leafEvaluator)
    
let expansion (s: State, i) =
    match s.leaves.[i] with
    | Unexplored a ->
        let state = State(Parent(s), a.DoCoreAction())
        s.leaves.[i] <-
         if Array.isEmpty state.leaves
         then Terminal(state.state.PlayerTurn = Player.Player1)
         else Leaf(state)
        state
    | _ -> raise(Exception("Target leaf is already expanded"))

let simulation (s: State) =
    let currentState = ref s.state
    let actions = ref (currentState.Value.Actions())
    let random = Random()
    while (not(Array.isEmpty(actions.Value))) do
        let nextMove = random.Next() % actions.Value.Length
        currentState := actions.Value.[nextMove].DoCoreAction()
        actions := currentState.Value.Actions()
    
    currentState.Value.PlayerTurn = Player.Player1

let backPropagating (s: State) win =
    let increment = if(win) then (fun (s: State) -> s.registerWin()) else (fun (s: State) -> s.registerLoss())
    let i = ref s
    let mutable rootReached = false
    while not rootReached do
        match i.Value.parent with
        | Parent.None -> rootReached <- true
        | Parent.Parent p ->
            increment(p)
            i:= p

let extractionEvaluator (p: Player, l: Leaf) =
    match l with
    | Terminal win -> if(win && p = Player.Player1 || (not win && p = Player.Player2)) then 1. else 0.
    | Leaf s -> if(p = Player.Player1) then s.winRate else 1. - s.winRate
    | Unexplored _ -> 0.
    
let extractBestPath (s: State) =
    let mutable path = List.empty
    let currentState = ref s
    let mutable endReached = false
    while not endReached do 
        if Array.isEmpty s.leaves
        then endReached <- true
        else
            let bestAction = currentState.Value.leaves |> Array.indexed |> Array.maxBy (fun l -> extractionEvaluator(currentState.Value.state.PlayerTurn, snd l))
            path <- (fst(bestAction)) :: path
            match snd bestAction with
            | Leaf nextState -> currentState := nextState
            | _ -> endReached <- true
    
    path |> List.rev

let search(root: State, timer: Stopwatch, evaluateUntil: Int64) =
    while timer.ElapsedTicks < evaluateUntil do
        match selection(root, leafEvaluator) with
        | Exhausted s ->
            let win = simulation s
            backPropagating s win
        | Candidate(c, a) ->
            let ex = expansion(c, a)
            let win = simulation ex
            backPropagating ex win
    
    extractBestPath root |> List.toArray