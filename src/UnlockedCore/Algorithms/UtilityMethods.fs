module UnlockedCore.Algorithms.UtilityMethods

open System
open UnlockedCore
open UnlockedCore.Algorithms.Accumulator
open AISupportTypes

let plyLimit = 1000

let changingPlayer (s: ICoreState) =
    s.PreviousState.IsSome
    && s.PlayerTurn
    <> s.PreviousState.Value.PlayerTurn


let toRemainingSearch (depth: searchLimit) tn =
                        match depth with
                        | searchLimit.Ply(plies, timeLimit) -> (Plies plies, timeLimit)
                        | searchLimit.Turn(turns, timeLimit) -> (Turns (tn + turns, 0), timeLimit)
let reduceRemainingSearch =
    function
    | Turns (turn, depth) ->
        if (depth >= plyLimit)
        then failwith (sprintf "search depth exceeded %i" plyLimit)
        else Turns(turn, depth + 1)
    | Plies remaining -> Plies(remaining - 1)

let increaseRemainingSearch =
    function
    | Turns (turn, _) -> Turns(turn + 1, 0)
    | Plies remaining -> Plies(remaining + 1)

let startRemainingSearch (s: ICoreState) =
    function
    | Turns _ -> Turns(s.TurnNumber, plyLimit)
    | Plies _ -> Plies(0)
    
let isDepthReached current target =
    match current with
    | Turns (turn, _) ->
        match target with
        | Turns (t, _) -> turn >= t
        | Plies _ -> raise (ArgumentException "Type mismatch") 
    | Plies (remaining) ->
        match target with
        | Plies (r) -> remaining >= r
        | Turns _ -> raise (ArgumentException "Type mismatch")

let flipValue v = (-fst v, snd v)

let startColor =
    fun (s: ICoreState) -> if (s.PlayerTurn = Player.Player1) then 1 else -1

let nextPvAction pv actions =
    let nextMove = pv |> List.head
    let pvAction = Seq.find (fun e -> (snd e) = nextMove) actions
    (fst pvAction, snd pvAction, List.skip 1 pv)
    
let doIncrementalSearch
    (aiAlgorithm: remainingSearch -> ICoreState -> accumulator -> int list -> int * int list)
    (targetLimit: remainingSearch)
    (s: ICoreState)
    (acc: accumulator)=
    let mutable currentLimit = startRemainingSearch s targetLimit
    let mutable previousResult = (-Int32.MaxValue, List<int>.Empty)
    while(not (isDepthReached currentLimit targetLimit)) do
        currentLimit <- increaseRemainingSearch currentLimit
        previousResult <- aiAlgorithm currentLimit s acc (snd previousResult)
        
    snd previousResult