module UnlockedCore.Algorithms.Accumulator

open UnlockedCore

let logPrunedPaths (logConfig: LoggingConfiguration) = logConfig.HasFlag(LoggingConfiguration.LogPrunedPaths)

type accumulator(evaluator : ICoreState -> int, logConfig: LoggingConfiguration, ?searchConfig0: SearchConfiguration) =
    let _searchConfig = defaultArg searchConfig0 SearchConfiguration.NoRestrictions
    let mutable _prunedPaths = 0
    let mutable _successfulHashMapLookups = 0
        
    let _incrementPrunedPaths =
        if(logPrunedPaths logConfig)
        then fun () -> _prunedPaths <- _prunedPaths + 1
        else fun () -> ()
        
    member this.incrementPrunedPaths () = _incrementPrunedPaths ()
    member this.logConfig = logConfig
    member this.eval = evaluator
    member this.prunedPaths
        with get() = _prunedPaths
    member this.successfulHashMapLookups
        with get() = _successfulHashMapLookups
        and set(value) = _successfulHashMapLookups <- value
    member this.searchConfig
        with get() = _searchConfig