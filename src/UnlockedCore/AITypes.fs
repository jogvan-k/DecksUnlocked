namespace ClassLibrary1.AITypes

open System
open UnlockedCore

type SearchDepthConfiguration =
    | node = 0
    | turn = 1

[<Flags>]
type LoggingConfiguration =
    | NoLogging = 0
    | LogEvaluatedStates = 1
    | LogTime = 2
    | LogAll = 3

module UtilMethods =
    let toSearchLimit searchConfiguration depth tn =
                        match searchConfiguration with
                        | SearchDepthConfiguration.node -> AIMethods.Depth depth
                        | SearchDepthConfiguration.turn -> AIMethods.Until (tn + depth)
                        | _ ->  raise (ArgumentException("Undefined SearchDepthConfiguration"))

[<Struct>]
type LogInfo =
    { mutable NodesEvaluated: int
      mutable ElapsedTime: TimeSpan }

type MinimaxAI(evaluator : IEvaluator, depth, searchConfiguration: SearchDepthConfiguration, loggingConfiguration : LoggingConfiguration) =
    
    [<DefaultValue>] val mutable logInfo: LogInfo
    interface IGameAI with
        member this.DetermineAction s =
            let d = UtilMethods.toSearchLimit searchConfiguration depth s.TurnNumber
            
            let logEvaulatedStates = loggingConfiguration.HasFlag(LoggingConfiguration.LogEvaluatedStates)
            let logTime = loggingConfiguration.HasFlag(LoggingConfiguration.LogTime)
            
            let evaluate =
                if(logEvaulatedStates)
                then
                    function i-> this.logInfo.NodesEvaluated <- this.logInfo.NodesEvaluated + 1
                                 evaluator.Evaluate i
                else evaluator.Evaluate
            
            let timer = if(logTime)
                        then Some(System.Diagnostics.Stopwatch.StartNew())
                        else None
            
            let returnVal = AIMethods.minimaxAI evaluate d s |> snd |> List.toArray
            
            this.logInfo.ElapsedTime <- if(timer.IsSome)
                                        then timer.Value.Stop()
                                             timer.Value.Elapsed
                                        else TimeSpan()
            
            returnVal

type RandomMoveAI() =
    let rng = Random()

    interface IGameAI with
        member this.DetermineAction s =
            AIMethods.randomMoveAI rng s |> Array.singleton
