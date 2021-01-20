﻿module UnlockedCore.MCTS.AI

open System
open System.Collections.Generic
open System.Diagnostics
open UnlockedCore
open UnlockedCore.MCTS.Algorithm
open UnlockedCore.MCTS.Types

type configuration =
    | None
    | TranspositionTable

let tTable (config: configuration) =
    match config with
    | TranspositionTable -> Some(HashSet<int>() :> ISet<int>)
        | None -> option.None

type MonteCarloTreeSearch(st: searchTime, config: configuration) =
    let mutable _logInfos: LogInfo list = List.empty
    let _evaluateUntil = match st with
                         | Minutes s -> Stopwatch.Frequency * int64(60 * s)
                         | Seconds s -> Stopwatch.Frequency * int64(s)
                         | MilliSeconds s -> Stopwatch.Frequency / int64(1000) * int64(s)
                         | Unlimited -> Int64.MaxValue

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
            let root = State(Parent.None, state)
            let tTable = tTable config
            
            let result = search(root, timer, tTable, _evaluateUntil)
            
            let mutable logInfo = LogInfo()
            logInfo.simulations <- root.visitCount
            logInfo.elapsedTime <- timer.Elapsed
            logInfo.estimatedAiWinChance <- extractWinChance root
            _logInfos <- logInfo :: _logInfos
            result
    
    member this.LatestLogInfo() = List.head _logInfos