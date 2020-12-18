module UnlockedCore.AI.AIBase

open System
open UnlockedCore
open UnlockedCore.Algorithms.Accumulator
open UnlockedCore.Algorithms.UtilityMethods

[<AbstractClass>]
type BaseAI(evaluator: IEvaluator,
            depth,
            searchDepthConfig: SearchDepthConfiguration,
            searchConfig: SearchConfiguration,
            loggingConfiguration: LoggingConfiguration) =
    let logEvaulatedStates =
        loggingConfiguration.HasFlag(LoggingConfiguration.LogEvaluatedStates)

    let logTime =
        loggingConfiguration.HasFlag(LoggingConfiguration.LogTime)

    let mutable _logInfos: LogInfo list = List.empty
    abstract AICall: searchLimit -> ICoreState -> accumulator -> int array
    member this.logInfos = _logInfos
    member this.LatestLogInfo = this.logInfos.Head

    interface IGameAI with
        member this.DetermineAction s =
            let mutable logInfo = LogInfo()

            let timer =
                if (logTime)
                then Some(System.Diagnostics.Stopwatch.StartNew())
                else None

            let d =
                toSearchLimit searchDepthConfig depth s.TurnNumber

            let evaluate =
                if (logEvaulatedStates) then
                    function
                    | i ->
                        logInfo.nodesEvaluated <- logInfo.nodesEvaluated + 1
                        evaluator.Evaluate i
                else
                    evaluator.Evaluate

            let accumulator = accumulator (evaluate, loggingConfiguration, searchConfig)
            let returnVal = this.AICall d s accumulator

            logInfo.elapsedTime <-
                match timer with
                | Some (timer) ->
                    timer.Stop()
                    timer.Elapsed
                | None -> TimeSpan()

            logInfo.prunedPaths <- accumulator.prunedPaths
            logInfo.successfulHashMapLookups <- accumulator.successfulHashMapLookups

            _logInfos <- logInfo :: _logInfos
            returnVal