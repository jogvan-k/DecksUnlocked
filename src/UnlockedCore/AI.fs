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
              then failwith "search depth exceeded %i" seachDepthLimit
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
    | NoLogging          = 0x0
    | LogEvaluatedStates = 0x1
    | LogTime            = 0x2
    | LogPrunedPaths     = 0x4
    | LogAll             = 7
    
type accumulator(evaluator : ICoreState -> int, logConfig: LoggingConfiguration) =
    let mutable _prunedPaths = 0
    member this.logConfig = logConfig
    member this.eval = evaluator
    member this.prunedPaths
        with get() = _prunedPaths
        and set v = _prunedPaths <- v
    
let logPrunedPaths (acc: accumulator) = acc.logConfig.HasFlag(LoggingConfiguration.LogPrunedPaths)

let rec recMinimaxAI (acc: accumulator) (depth: searchLimit) alpha beta (s: ICoreState) =
    match s with
    | SearchLimit depth ex
    | Terminal ex -> (acc.eval s, [])
    | Node actions ->
        let iter = actions.GetEnumerator()
        let mutable i = 0
        iter.MoveNext() |> ignore
        let mutable currentBest = recMinimaxAI acc (reduce depth) alpha beta (iter.Current.DoCoreAction())
        currentBest <- (fst currentBest, i :: snd currentBest)
        if (s.PlayerTurn = Player.Player1)
        then
            let mutable alpha' = max alpha (fst currentBest)
            while alpha' < beta && iter.MoveNext() do
                i <- i + 1
                let candidate = recMinimaxAI acc (reduce depth) alpha' beta (iter.Current.DoCoreAction())
                if (fst candidate) > (fst currentBest)
                then currentBest <- (fst candidate, i :: snd candidate)
                     alpha' <- max alpha (fst candidate)
            
        else
            let mutable beta' = min beta (fst currentBest)
            while beta' > alpha && iter.MoveNext() do
                i <- i + 1
                let candidate = recMinimaxAI acc (reduce depth) alpha beta' (iter.Current.DoCoreAction())
                if (fst candidate) < (fst currentBest)
                then currentBest <- (fst candidate, i :: snd candidate)
                     beta' <- min beta (fst candidate)
        if(logPrunedPaths acc) then
            acc.prunedPaths <- acc.prunedPaths + 1
        currentBest
let minimaxAI (acc: accumulator) (depth: searchLimit) (s: ICoreState) =
    recMinimaxAI acc depth Int32.MinValue Int32.MaxValue s

let randomMoveAI (rng: Random) (s: ICoreState) =
    let actions = s.Actions()
    rng.Next() % actions.Length
