module AIMethods

open System
open UnlockedCore

let seachDepthLimit = 100
type searchLimit =
    | Until of turn: int * depth: int
    | Depth of remaining: int
    
type SearchConfiguration =
    | NoRestrictions = 0
    | NoHashTable = 1


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

let changingPlayer (s: ICoreState) =
    s.PreviousState.IsSome && s.PlayerTurn <> s.PreviousState.Value.PlayerTurn
    
let (|Node|Terminal|) (s: ICoreState) =
    let actions = s.Actions() |> Array.mapi (fun i a -> (a, i))
    if (actions.Length = 0)
    then Terminal 0
    else Node(actions |> Array.toSeq |> Seq.sortBy fst)

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
let avoidHashMap (searchConfig: SearchConfiguration) = searchConfig.HasFlag(SearchConfiguration.NoHashTable)
let logSuccessfulHashMapLookup (logConfig: LoggingConfiguration) = logConfig.HasFlag(LoggingConfiguration.LogSuccessfulHashMapLookup)

type accumulator(evaluator : ICoreState -> int, logConfig: LoggingConfiguration, ?searchConfig0: SearchConfiguration) =
    let searchConfig = defaultArg searchConfig0 SearchConfiguration.NoRestrictions
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
        
    let _addToHashMap =
        if(avoidHashMap searchConfig)
        then fun (hash, value) -> ()
        else fun (hash, value) -> _hashMap <- _hashMap.Add(hash, value)
    member this.incrementPrunedPaths () = _incrementPrunedPaths ()
    member this.logConfig = logConfig
    member this.eval = evaluator
    member this.addToHashMap hash value = _addToHashMap(hash, value)
    member this.lookupHashMap hash = _lookupHashMap hash
    member this.prunedPaths
        with get() = _prunedPaths
    member this.successfulHashMapLookups
        with get() = _successfulHashMapLookups

let rec recNegamaxAI alpha beta (acc: accumulator) (depth: searchLimit) (s: ICoreState) color =
    match s with
    | SearchLimit depth _
    | Terminal _ -> (color * acc.eval s, [])
    | Node (actions) ->
        let stateHash = s.GetHashCode()
        let hashMapLookup = acc.lookupHashMap stateHash
        if(hashMapLookup.IsSome)
        then hashMapLookup.Value
        else
            let iter = actions.GetEnumerator()
            iter.MoveNext() |> ignore
            let currentIter = iter.Current
            let mutable currentBest = nextCandidate alpha beta acc (reduce depth) ((fst currentIter).DoCoreAction()) color
            currentBest <- (fst currentBest, snd currentIter :: snd currentBest)
            let mutable alpha' = max alpha (fst currentBest)
            while iter.MoveNext() do
                let currentIter = iter.Current
                if(alpha' < beta)
                then let candidate = nextCandidate alpha' beta acc (reduce depth) ((fst currentIter).DoCoreAction()) color
                     if (fst candidate) > (fst currentBest)
                     then currentBest <- (fst candidate, snd currentIter :: snd candidate)
                          alpha' <- max alpha' (fst candidate)
                else acc.incrementPrunedPaths()

            acc.addToHashMap stateHash currentBest
            currentBest
and nextCandidate alpha beta acc depth s color =
    if(changingPlayer s)
        then let (fs, sn) = recNegamaxAI (-1 * beta) (-1 * alpha) acc depth s (-color)
             (-fs, sn)
        else recNegamaxAI alpha beta acc depth s color

let negamaxAI (acc: accumulator) (depth: searchLimit) =
    recNegamaxAI (Int32.MinValue + 1) Int32.MaxValue acc depth

let randomMoveAI (rng: Random) (s: ICoreState) =
    let actions = s.Actions()
    rng.Next() % actions.Length
