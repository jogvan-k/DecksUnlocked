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
    let logEvaulatedStates = loggingConfiguration.HasFlag(LoggingConfiguration.LogEvaluatedStates)
    let logTime = loggingConfiguration.HasFlag(LoggingConfiguration.LogTime)
    let incrementalSearch = searchConfig.HasFlag(SearchConfiguration.IncrementalSearch)
    let mutable _logInfos: LogInfo list = List.empty
    abstract AICall: searchLimit -> ICoreState -> accumulator -> int list -> int * int list
    
    member this.logInfos = _logInfos
    member this.LatestLogInfo = this.logInfos.Head

    interface IGameAI with
        member this.DetermineAction (s: ICoreState) = (this :> IGameAI).DetermineActionWithVariation s Array.empty
        member this.DetermineActionWithVariation (s: ICoreState) (v: int[]) =
            let mutable logInfo = LogInfo()

            let timer =
                if (logTime)
                then Some(System.Diagnostics.Stopwatch.StartNew())
                else None

            let d = toSearchLimit searchDepthConfig depth s.TurnNumber

            let evaluate =
                if (logEvaulatedStates) then
                    function
                    | i ->
                        logInfo.nodesEvaluated <- logInfo.nodesEvaluated + 1
                        evaluator.Evaluate i
                else
                    evaluator.Evaluate

            let accumulator = accumulator (evaluate, loggingConfiguration, searchConfig)
            
            let returnVal = if(incrementalSearch)
                            then doIncrementalSearch this.AICall d s accumulator
                            else snd (this.AICall d s accumulator (v |> Array.toList))

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
            