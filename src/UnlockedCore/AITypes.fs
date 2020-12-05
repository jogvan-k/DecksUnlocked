namespace UnlockedCore.AITypes

open System
open UnlockedCore
open AIMethods

type SearchDepthConfiguration =
    | actions = 0
    | turn = 1

module UtilMethods =
    let toSearchLimit searchConfiguration depth tn =
                        match searchConfiguration with
                        | SearchDepthConfiguration.actions -> Depth depth
                        | SearchDepthConfiguration.turn -> Until (tn + depth, 0)
                        | _ ->  raise (ArgumentException("Undefined SearchDepthConfiguration"))

type LogInfo =
    struct
        val mutable nodesEvaluated: int
        val mutable elapsedTime: TimeSpan
        val mutable prunedPaths: int
        val mutable successfulHashMapLookups: int
    end

type MinimaxAI(evaluator : IEvaluator, depth, searchDepthConfig: SearchDepthConfiguration, ?searchConfig0: SearchConfiguration, ?loggingConfiguration0 : LoggingConfiguration) =
    let searchConfig = defaultArg searchConfig0 SearchConfiguration.NoRestrictions
    let loggingConfiguration = defaultArg loggingConfiguration0 LoggingConfiguration.LogAll
    let logEvaulatedStates = loggingConfiguration.HasFlag(LoggingConfiguration.LogEvaluatedStates)
    let logTime = loggingConfiguration.HasFlag(LoggingConfiguration.LogTime)
    let mutable _logInfos: LogInfo list = List.empty
    member this.logInfos
        with get () = _logInfos
        
    member this.LatestLogInfo = this.logInfos.Head
    
    interface IGameAI with
        member this.DetermineAction s =
            let mutable logInfo = LogInfo()
            let timer = if(logTime)
                        then Some(System.Diagnostics.Stopwatch.StartNew())
                        else None
            
            let d = UtilMethods.toSearchLimit searchDepthConfig depth s.TurnNumber
            
            let evaluate =
                if(logEvaulatedStates)
                then
                    function i-> logInfo.nodesEvaluated <- logInfo.nodesEvaluated + 1
                                 evaluator.Evaluate i
                else evaluator.Evaluate
            
            let accumulator = accumulator(evaluate, loggingConfiguration, searchConfig)
            let color = if(s.PlayerTurn = Player.Player1) then 1 else -1
            let returnVal = minimaxAI accumulator d s color |> snd |> List.toArray

            logInfo.elapsedTime <- if(timer.IsSome)
                                        then timer.Value.Stop()
                                             timer.Value.Elapsed
                                        else TimeSpan()
            logInfo.prunedPaths <- accumulator.prunedPaths
            logInfo.successfulHashMapLookups <- accumulator.successfulHashMapLookups
            
            _logInfos <- logInfo :: _logInfos
            returnVal

type RandomMoveAI() =
    let rng = Random()

    interface IGameAI with
        member this.DetermineAction s =
            randomMoveAI rng s |> Array.singleton
