module UnlockedCore.MCTS.Algorithm

open System

open System.Collections.Generic
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
let explorationConstant = sqrt(2.)
let explorationRate(stateVisitCount: int, actionVisitCount: int) =
    explorationConstant * sqrt(log(float(stateVisitCount)) / float(actionVisitCount))

let leafEvaluator (state: State, l: Leaf) =
    let actingPlayer = state.playerTurn
    match l with
    | Terminal win -> if actingPlayer = win then 100. else 0.
    | Unexplored _ -> 10.
    | Leaf a -> a.winRate + explorationRate(state.visitCount, a.visitCount)
    | Duplicate -> 0.


let rec recSelection (s: State, actionHistory: Action list, leafEvaluator) =
    if (Array.isEmpty(s.leaves)) then Exhausted(actionHistory, s.playerTurn)
    else
    match s.leaves |> Array.indexed |>  Array.maxBy (fun i -> leafEvaluator(s, snd i)) with
    | (_, Terminal win) -> Exhausted(actionHistory, win)
    | (i, Unexplored _) -> Candidate(actionHistory, i)
    | (_, Leaf ls) -> recSelection(ls.state, ls :: actionHistory, leafEvaluator)
    | (_, Duplicate) -> raise(Exception("TODO remove Duplicate"))

let selection (s: State, leafEvaluator) =
    recSelection(s, [], leafEvaluator)
    
let expandUnexplored (parent: State, i, nextState: ICoreState) =
    let state = State(nextState)
    let leaf = if Array.isEmpty state.leaves
                     then Terminal(state.playerTurn)
                     else let a = Action(parent.playerTurn, state)
                          Leaf(a)
    parent.leaves.[i] <- leaf
    leaf

let expansion (s: State, i, tTable: int ISet Option) =
    match s.leaves.[i] with
    | Unexplored a ->
        let nextState = a.DoCoreAction()
        expandUnexplored(s, i, nextState)
    | _ -> raise(Exception("Target leaf is already expanded"))

let simulation (s: State) =
    let currentState = ref s.state
    let actions = ref (currentState.Value.Actions())
    while (not(Array.isEmpty(actions.Value))) do
        let nextMove = actions.Value |> Seq.sort |> Seq.head
        currentState := nextMove.DoCoreAction()
        actions := currentState.Value.Actions()
    
    currentState.Value.PlayerTurn

let registerResult (s: State) (playerWin: Player) =
    if s.state.PlayerTurn = playerWin
        then s.registerWin()
        else s.registerLoss()

let backPropagating (root: State) (a: Action list) (playerWin: Player) =
    for a1 in a do
        a1.incrementVisitCount()
        registerResult a1.state playerWin
    registerResult root playerWin

let extractionEvaluator (p: Player, l: Leaf) =
    match l with
    | Terminal win -> if(p = win) then 1. else 0.
    | Leaf a -> a.winRate
    | Unexplored _ -> 0.
    | Duplicate -> 0.
    
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
            | Leaf action -> currentState := action.state
            | _ -> endReached <- true
    
    path |> List.rev

let search(root: State, maxSimulationCount, timer: Stopwatch, tTable, evaluateUntil: Int64 option) =
    while(root.visitCount < maxSimulationCount
          && (not evaluateUntil.IsSome || timer.ElapsedTicks < evaluateUntil.Value)) do
        match selection(root, leafEvaluator) with
        | Exhausted (actionHistory, win) ->
            backPropagating root actionHistory win
        | Candidate(actionHistory, a) ->
            let s = if List.isEmpty actionHistory then root else actionHistory.[0].state
            match expansion(s, a, tTable) with
            | Leaf a ->
                        let win = simulation a.state
                        backPropagating root (a :: actionHistory) win
            | Terminal win -> backPropagating root actionHistory win
            | _ -> raise(Exception("Expanded to unexpected leaf type"))
    
    extractBestPath root |> List.toArray
    
let parallelSearch(root: State, maxSimulationCount, tTable, evaluateUntil: int) =
    let expression = async {
        let (leaf, ah) = lock root (fun () ->
            match selection(root, leafEvaluator) with
            | Exhausted a -> (Terminal(snd a), fst a)
            | Candidate(ah, i) ->
                let s = if List.isEmpty ah then root else ah.[0].state
                (expansion(s, i, option.None), ah))
        
        let (win, actionHistory) =
            match leaf with
            | Leaf a -> (simulation a.state, a :: ah)
            | Terminal win -> (win, ah)
            
        lock root (fun () -> backPropagating root actionHistory win)
        }
    
    try
        let tasks = Async.Parallel [ for _ in 1..maxSimulationCount -> expression ]
        Async.RunSynchronously(tasks, evaluateUntil)
        |> ignore
    with
    | :? TimeoutException -> ()
    
    extractBestPath root |> List.toArray