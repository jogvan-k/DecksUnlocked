module UnlockedCore.Algorithms.Accumulator

open System.Diagnostics
open UnlockedCore

let logPrunedPaths (logConfig: LoggingConfiguration) = logConfig.HasFlag(LoggingConfiguration.LogPrunedPaths)

type accumulator(evaluator : ICoreState -> int, st: searchTime, logConfig: LoggingConfiguration, ?searchConfig0: SearchConfiguration) =
    let _searchConfig = defaultArg searchConfig0 SearchConfiguration.NoRestrictions
    let mutable _prunedPaths = 0
    let mutable _successfulHashMapLookups = 0
    let _timer = Stopwatch()
    let mutable _timeLimitReached = false
    let _evaluateUntil = match st with
                         | Unlimited -> None
                         | Minutes s -> Some(Stopwatch.Frequency * int64(60 * s))
                         | Seconds s -> Some(Stopwatch.Frequency * int64(s))
                         | MilliSeconds s -> Some(Stopwatch.Frequency / int64(1000) * int64(s))
        
        
    let _incrementPrunedPaths =
        if(logPrunedPaths logConfig)
        then fun () -> _prunedPaths <- _prunedPaths + 1
        else fun () -> ()
        
    member this.incrementPrunedPaths () = _incrementPrunedPaths ()
    member this.logConfig = logConfig
    member this.eval (s: ICoreState) =
        if(_evaluateUntil.IsSome && _timer.ElapsedTicks > _evaluateUntil.Value)
        then _timeLimitReached <- true
        evaluator s

    member this.prunedPaths
        with get() = _prunedPaths
    member this.successfulHashMapLookups
        with get() = _successfulHashMapLookups
        and set(value) = _successfulHashMapLookups <- value
    member this.searchConfig
        with get() = _searchConfig
    
    member this.startTimer = _timer.Start()
    member this.isTimeLimitReached = _timeLimitReached