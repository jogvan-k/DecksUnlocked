namespace UnlockedCore.Test.BenchmarkTest

open System
open UnlockedCore.AITypes
open NUnit.Framework
open UnlockedCore
open UnlockedCore.TestTypes

[<TestFixture>]
type BenchmarkCases () =
    
    let evaluate evalFun n b =
        let tree = complexTree evalFun n b
        let cal = NegamaxAI(evaluator, n, SearchDepthConfiguration.turn)
        
        let result = (cal :> IGameAI).DetermineAction (tree.build())
        let logInfo = cal.LatestLogInfo
        
        let treeSize = ((pown b (n + 1)) - 1) / (b - 1)
        let leafNodes = pown b n
        
        printfn "Searched tree of depth %i, branch width %i, %i total nodes and %i branch nodes " n b treeSize leafNodes
        printfn "%i nodes evaluated in %i hours %i minutes and %i seconds %i milliseconds" logInfo.nodesEvaluated logInfo.elapsedTime.Hours logInfo.elapsedTime.Minutes logInfo.elapsedTime.Seconds logInfo.elapsedTime.Milliseconds
        printfn "%i paths pruned and %i successful hash table lookups" cal.LatestLogInfo.prunedPaths cal.LatestLogInfo.successfulHashMapLookups
        
        logInfo
    
    [<Explicit>]
    [<Test>]
    member this.AllUniqueNodes () =
        let rng = Random()
        let evalFun (d,h) = (Player.Player1, d, h, rng.Next())
        let n, b = 8, 6
        
        let logInfo = evaluate evalFun n b
        
        Assert.That(logInfo.nodesEvaluated, Is.EqualTo(pown b n))
        Assert.That(logInfo.prunedPaths, Is.EqualTo(0))
        Assert.That(logInfo.successfulHashMapLookups, Is.EqualTo(0))
        
    [<Explicit>]
    [<Test>]
    member this.PathsPruned () =
        let rng = Random()
        let evalFun (d,h) =
            let playerTurn = if(d % 2 = 0) then p1 else p2
            (playerTurn, d, rng.Next() / 100, rng.Next())
        let n, b = 8, 6
        
        let logInfo = evaluate evalFun n b
        
        Assert.That(logInfo.nodesEvaluated, Is.GreaterThan(0))
        Assert.That(logInfo.prunedPaths, Is.GreaterThan(0))
        Assert.That(logInfo.successfulHashMapLookups, Is.EqualTo(0))
        
    [<Explicit>]
    [<Test>]
    member this.HashMapUsed () =
        let rng = Random()
        let evalFun (d,h) = (p1, d, h, rng.Next() / 100)
        let n, b = 8, 6
        
        let logInfo = evaluate evalFun n b
        
        Assert.That(logInfo.nodesEvaluated, Is.GreaterThan(0))
        Assert.That(logInfo.prunedPaths, Is.EqualTo(0))
        Assert.That(logInfo.successfulHashMapLookups, Is.GreaterThan(0))