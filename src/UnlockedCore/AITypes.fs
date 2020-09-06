namespace UnlockedCore.AITypes

open System
open UnlockedCore
open AIMethods

type SearchDepthConfiguration =
    | node = 0
    | turn = 1

module UtilMethods =
    let toSearchLimit searchConfiguration depth tn =
                        match searchConfiguration with
                        | SearchDepthConfiguration.node -> Depth depth
                        | SearchDepthConfiguration.turn -> Until (tn + depth, 0)
                        | _ ->  raise (ArgumentException("Undefined SearchDepthConfiguration"))

[<Struct>]
type LogInfo =
    { mutable nodesEvaluated: int
      mutable elapsedTime: TimeSpan
      mutable prunedPaths: int
      mutable successfulHashMapLookups: int}

type MinimaxAI(evaluator : IEvaluator, depth, searchConfiguration: SearchDepthConfiguration, loggingConfiguration : LoggingConfiguration) =
    
    [<DefaultValue>] val mutable logInfo: LogInfo
    let logEvaulatedStates = loggingConfiguration.HasFlag(LoggingConfiguration.LogEvaluatedStates)
    let logTime = loggingConfiguration.HasFlag(LoggingConfiguration.LogTime)
    let timer = if(logTime)
                then Some(System.Diagnostics.Stopwatch.StartNew())
                else None

    interface IGameAI with
        member this.DetermineAction s =
            let d = UtilMethods.toSearchLimit searchConfiguration depth s.TurnNumber
            
            let evaluate =
                if(logEvaulatedStates)
                then
                    function i-> this.logInfo.nodesEvaluated <- this.logInfo.nodesEvaluated + 1
                                 evaluator.Evaluate i
                else evaluator.Evaluate
            
            let accumulator = accumulator(evaluate, loggingConfiguration)
            let returnVal = minimaxAI accumulator d s |> snd |> List.toArray

            this.logInfo.elapsedTime <- if(timer.IsSome)
                                        then timer.Value.Stop()
                                             timer.Value.Elapsed
                                        else TimeSpan()
            this.logInfo.prunedPaths <- accumulator.prunedPaths
            this.logInfo.successfulHashMapLookups <- accumulator.successfulHashMapLookups
            
            returnVal

type RandomMoveAI() =
    let rng = Random()

    interface IGameAI with
        member this.DetermineAction s =
            AIMethods.randomMoveAI rng s |> Array.singleton
