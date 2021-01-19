module UnlockedCore.MCTS.MonteCarloTreeSearch

open System
open System.Diagnostics
open UnlockedCore
open UnlockedCore.MCTS.Algorithm
open UnlockedCore.MCTS.Types

type MonteCarloTreeSearch(st: searchTime) =
    let mutable _logInfos: LogInfo list = List.empty
    let _evaluateUntil = match st with
                         | Minutes s -> Stopwatch.Frequency * int64(60 * s)
                         | Seconds s -> Stopwatch.Frequency * int64(s)
                         | MilliSeconds s -> Stopwatch.Frequency / int64(1000) * int64(s)
                         | Unlimited -> Int64.MaxValue

    interface IGameAI with
        member this.DetermineAction(state) =
            let timer = Stopwatch.StartNew()
            let root = State(Parent.None, state)
            
            let result = search(root, timer, _evaluateUntil)
            
            let mutable logInfo = LogInfo()
            logInfo.simulations <- root.visitCount
            logInfo.elapsedTime <- timer.Elapsed
            _logInfos <- logInfo :: _logInfos
            result
    
    member this.LatestLogInfo() = List.head _logInfos