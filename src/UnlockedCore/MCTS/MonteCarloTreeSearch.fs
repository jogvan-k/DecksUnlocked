module UnlockedCore.MCTS.AI

open System
open System.Collections
open System.Collections.Generic
open System.Diagnostics
open UnlockedCore
open UnlockedCore.Algorithms
open UnlockedCore.MCTS.Algorithm
open UnlockedCore.MCTS.Types

[<Flags>]
type configuration =
    | None               = 0x0
    | TranspositionTable = 0x1
    | AsyncExecution     = 0x2
    | All                = 0x3

let tTable (config: configuration) =
    if(config.HasFlag(configuration.TranspositionTable))
    then Some(TranspositionTable())
    else option.None

type MonteCarloTreeSearch(st: searchTime, maxSimulationCount, config: configuration) =
    let mutable _logInfos: LogInfo list = List.empty

    let extractWinChance (s: State) =
       let aiPlayer = s.state.PlayerTurn
       if(s.leaves |> Array.isEmpty)
       then s.winRate
       else  s.leaves
             |> Array.map (fun i -> extractionEvaluator(aiPlayer, i))
             |> Array.max
        
    interface IGameAI with
        member this.DetermineAction(state) =
            let timer = Stopwatch.StartNew()
            let root = State(state)
            let tTable = tTable config
            
            let result =
                if config.HasFlag(configuration.AsyncExecution)
                then parallelSearch(root, maxSimulationCount, tTable, Utility.toMilliseconds st)
                else search(root, maxSimulationCount, timer, tTable, Utility.toStopwatchTics st)
            
            let mutable logInfo = LogInfo()
            logInfo.simulations <- root.visitCount
            logInfo.elapsedTime <- timer.Elapsed
            logInfo.estimatedAiWinChance <- extractWinChance root
            match tTable with
            | Some t -> logInfo.successfulTranspositionTableLookup <- t.SuccessfulLookups
                        logInfo.transpositionTableSize <- t.Count
            | None -> () 
            _logInfos <- logInfo :: _logInfos
            result
    
    member this.LatestLogInfo() = List.head _logInfos