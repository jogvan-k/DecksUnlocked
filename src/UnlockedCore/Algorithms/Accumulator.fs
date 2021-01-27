module UnlockedCore.Algorithms.Accumulator

open System.Diagnostics
open UnlockedCore
open UnlockedCore.AI.MinimaxTypes

let logPrunedPaths (logConfig: LoggingConfiguration) = logConfig.HasFlag(LoggingConfiguration.LogPrunedPaths)
let logCalculatedSteps (logConfig: LoggingConfiguration) = logConfig.HasFlag(LoggingConfiguration.LogCalculatedSteps);

type accumulator(evaluator : ICoreState -> int, st: searchTime, logConfig: LoggingConfiguration, ?searchConfig0: SearchConfiguration) =
    let _searchConfig = defaultArg searchConfig0 SearchConfiguration.NoRestrictions
    let mutable _prunedPaths = 0
    let mutable _successfulHashMapLookups = 0
    let mutable _stepsCalculated = 0
    let _timer = Stopwatch()
    let mutable _timeLimitReached = false
    let _evaluateUntil = Utility.toStopwatchTics st
        
    let _incrementPrunedPaths =
        if(logPrunedPaths logConfig)
        then fun () -> _prunedPaths <- _prunedPaths + 1
        else fun () -> ()
        
    let _nextState =
        if(logCalculatedSteps logConfig)
        then fun (a :ICoreAction) ->
            _stepsCalculated <- _stepsCalculated + 1
            a.DoCoreAction()
        else fun (a :ICoreAction) ->
            a.DoCoreAction()
        
        
    member this.incrementPrunedPaths () = _incrementPrunedPaths ()
    member this.nextState a = _nextState a
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
    member this.stepsCalculated = _stepsCalculated
    
    member this.startTimer = _timer.Start()
    member this.isTimeLimitReached = _timeLimitReached