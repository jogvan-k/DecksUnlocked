module UnlockedCore.Algorithms.UtilityMethods

open System
open UnlockedCore
open UnlockedCore.Algorithms.Accumulator
open UnlockedCore.Algorithms.TranspositionTable

let plyLimit = 1000

let changingPlayer (s: ICoreState) =
    s.PreviousState.IsSome
    && s.PlayerTurn
    <> s.PreviousState.Value.PlayerTurn

let toSearchLimit searchConfiguration depth tn =
                        match searchConfiguration with
                        | SearchDepthConfiguration.actions -> Ply depth
                        | SearchDepthConfiguration.turn -> Turn (tn + depth, 0)
                        | _ ->  raise (ArgumentException("Undefined SearchDepthConfiguration"))
let reduceSearchLimit =
    function
    | Turn (turn, depth) ->
        if (depth >= plyLimit)
        then failwith (sprintf "search depth exceeded %i" plyLimit)
        else Turn(turn, depth + 1)
    | Ply remaining -> Ply(remaining - 1)

let increaseSearchLimit =
    function
    | Turn (turn, _) -> Turn(turn + 1, 0)
    | Ply remaining -> Ply(remaining + 1)

let startSearchLimit (s: ICoreState) =
    function
    | Turn _ -> Turn(s.TurnNumber, plyLimit)
    | Ply _ -> Ply(0)
    
let isDepthReached current target =
    match current with
    | Turn (turn, _) ->
        match target with
        | Turn (t, _) -> turn >= t
        | Ply _ -> raise (ArgumentException "Type mismatch") 
    | Ply (remaining) ->
        match target with
        | Ply (r) -> remaining >= r
        | Turn _ -> raise (ArgumentException "Type mismatch")

let flipValue v = (-fst v, snd v)

let startColor =
    fun (s: ICoreState) -> if (s.PlayerTurn = Player.Player1) then 1 else -1

let nextPvAction pv actions =
    let nextMove = pv |> List.head
    let pvAction = Seq.find (fun e -> (snd e) = nextMove) actions
    (fst pvAction, snd pvAction, List.skip 1 pv)
    
let doIncrementalSearch
    (aiAlgorithm: searchLimit -> ICoreState -> accumulator -> int list -> int * int list)
    (targetLimit: searchLimit)
    (s: ICoreState)
    (acc: accumulator)=
    let mutable currentLimit = startSearchLimit s targetLimit
    let mutable previousResult = (-Int32.MaxValue, List<int>.Empty)
    while(not (isDepthReached currentLimit targetLimit)) do
        currentLimit <- increaseSearchLimit currentLimit
        previousResult <- aiAlgorithm currentLimit s acc (snd previousResult)
        
    snd previousResult