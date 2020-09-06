module AIMethods

open System
open UnlockedCore

let seachDepthLimit = 100
type searchLimit =
    | Until of turn: int * depth: int
    | Depth of remaining: int

let reduce =
    function
    | Until (turn, depth) ->
        if(depth >= seachDepthLimit)
              then failwith (sprintf "search depth exceeded %i" seachDepthLimit)
              else Until (turn, depth + 1)
    | Depth remaining -> Depth(remaining - 1)

type nodeStates =
    | Node
    | Terminal
    | SearchLimit

let (|Node|Terminal|) (s: ICoreState) =
    let actions = s.Actions()
    if (actions.Length = 0)
    then Terminal 0
    else Node(Array.toSeq actions)

let (|SearchLimit|_|) (limit: searchLimit) (s: ICoreState) =
    match limit with
    | Until (turn, _) when turn <= s.TurnNumber -> Some 0
    | Depth (remaining) when remaining <= 0 -> Some 0
    | _ -> None

    
[<Flags>]
type LoggingConfiguration =
    | NoLogging                    = 0x0
    | LogEvaluatedStates           = 0x1
    | LogTime                      = 0x2
    | LogSuccessfulHashMapLookup   = 0x4
    | LogPrunedPaths               = 0x8
    | LogAll                       = 0xf

let logPrunedPaths (logConfig: LoggingConfiguration) = logConfig.HasFlag(LoggingConfiguration.LogPrunedPaths)
let logSuccessfulHashMapLookup (logConfig: LoggingConfiguration) = logConfig.HasFlag(LoggingConfiguration.LogSuccessfulHashMapLookup)

type accumulator(evaluator : ICoreState -> int, logConfig: LoggingConfiguration) =
    let mutable _prunedPaths = 0
    let mutable _successfulHashMapLookups = 0
    let mutable _hashMap = Map.empty
    let _lookupHashMap: int -> (int * int list) option =
        if(logSuccessfulHashMapLookup logConfig)
        then fun hash -> let lookup = _hashMap.TryFind(hash)
                         if(lookup.IsSome)
                         then _successfulHashMapLookups <- _successfulHashMapLookups + 1
                         lookup
        else fun hash -> _hashMap.TryFind(hash)
        
    let _incrementPrunedPaths =
        if(logPrunedPaths logConfig)
        then fun () -> _prunedPaths <- _prunedPaths + 1
        else fun () -> ()
    member this.incrementPrunedPaths () = _incrementPrunedPaths ()
    member this.logConfig = logConfig
    member this.eval = evaluator
    member this.addToHashMap hash value = _hashMap <- _hashMap.Add(hash, value)
    member this.lookupHashMap hash = _lookupHashMap hash
    member this.prunedPaths
        with get() = _prunedPaths
    member this.successfulHashMapLookups
        with get() = _successfulHashMapLookups

let rec recMinimaxAI (acc: accumulator) (depth: searchLimit) alpha beta (s: ICoreState) =
    match s with
    | SearchLimit depth ex
    | Terminal ex -> (acc.eval s, [])
    | Node actions ->
        let stateHash = s.GetHashCode()
        let hashMapLookup = acc.lookupHashMap stateHash
        if(hashMapLookup.IsSome)
        then hashMapLookup.Value
        else
            let iter = actions.GetEnumerator()
            iter.MoveNext() |> ignore
            let mutable i = 0
            let mutable currentBest = recMinimaxAI acc (reduce depth) alpha beta (iter.Current.DoCoreAction())
            currentBest <- (fst currentBest, i :: snd currentBest)
            if (s.PlayerTurn = Player.Player1)
            then
                let mutable alpha' = max alpha (fst currentBest)
                while iter.MoveNext() do
                    if(alpha' < beta)
                    then i <- i + 1
                         let candidate = recMinimaxAI acc (reduce depth) alpha' beta (iter.Current.DoCoreAction())
                         if (fst candidate) > (fst currentBest)
                         then currentBest <- (fst candidate, i :: snd candidate)
                              alpha' <- max alpha (fst candidate)
                    else acc.incrementPrunedPaths()
            else
                let mutable beta' = min beta (fst currentBest)
                while iter.MoveNext() do
                    if(beta' > alpha)
                    then i <- i + 1
                         let candidate = recMinimaxAI acc (reduce depth) alpha beta' (iter.Current.DoCoreAction())
                         if (fst candidate) < (fst currentBest)
                         then currentBest <- (fst candidate, i :: snd candidate)
                              beta' <- min beta (fst candidate)
                    else acc.incrementPrunedPaths()

            acc.addToHashMap stateHash currentBest
            currentBest

let minimaxAI (acc: accumulator) (depth: searchLimit) (s: ICoreState) =
    recMinimaxAI acc depth Int32.MinValue Int32.MaxValue s

let randomMoveAI (rng: Random) (s: ICoreState) =
    let actions = s.Actions()
    rng.Next() % actions.Length
