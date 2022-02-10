module UnlockedCore.AI.AIBase

open System
open UnlockedCore
open UnlockedCore.AI.MinimaxTypes
open UnlockedCore.Algorithms.AISupportTypes
open UnlockedCore.Algorithms.Accumulator
open UnlockedCore.Algorithms.UtilityMethods

[<AbstractClass>]
type BaseAI
    (
        evaluator: IEvaluator,
        depth: searchLimit,
        searchConfig: SearchConfiguration,
        loggingConfiguration: LoggingConfiguration
    ) =
    let logEvaulatedStates =
        loggingConfiguration.HasFlag(LoggingConfiguration.LogEvaluatedEndStates)

    let logTime =
        loggingConfiguration.HasFlag(LoggingConfiguration.LogTime)

    let incrementalSearch =
        searchConfig.HasFlag(SearchConfiguration.IncrementalSearch)

    let mutable _logInfos: LogInfo list = List.empty
    abstract AICall : remainingSearch -> ICoreState -> accumulator -> int list -> int * int list

    member this.logInfos = _logInfos
    member this.LatestLogInfo = this.logInfos.Head

    interface IGameAI with
        member this.DetermineAction(s: ICoreState) =
            (this :> IGameAIWithVariationPath)
                .DetermineActionWithVariation
                s
                Array.empty

    interface IGameAIWithVariationPath with
        member this.DetermineActionWithVariation (s: ICoreState) (v: int []) =
            let mutable logInfo = LogInfo()

            let timer =
                if (logTime) then
                    Some(System.Diagnostics.Stopwatch.StartNew())
                else
                    None

            let (searchLimit, timeLimit) = toRemainingSearch depth s.TurnNumber

            let evaluate =
                if (logEvaulatedStates) then
                    function
                    | i ->
                        logInfo.endNodesEvaluated <- logInfo.endNodesEvaluated + 1
                        evaluator.Evaluate i
                else
                    evaluator.Evaluate

            let accumulator =
                accumulator (evaluate, timeLimit, loggingConfiguration, searchConfig)

            let returnVal =
                if (incrementalSearch) then
                    doIncrementalSearch this.AICall searchLimit s accumulator
                else
                    snd (this.AICall searchLimit s accumulator (v |> Array.toList))

            logInfo.stepsCalculated <- accumulator.stepsCalculated

            logInfo.elapsedTime <-
                match timer with
                | Some (timer) ->
                    timer.Stop()
                    timer.Elapsed
                | None -> TimeSpan()

            logInfo.prunedPaths <- accumulator.prunedPaths
            logInfo.successfulHashMapLookups <- accumulator.successfulHashMapLookups

            _logInfos <- logInfo :: _logInfos
            returnVal |> List.toArray
