namespace UnlockedCore.Test.BenchmarkTest

open System
open UnlockedCore.AI.MinimaxTypes
open UnlockedCore.AITypes
open NUnit.Framework
open UnlockedCore
open UnlockedCoreTest.TestTypes

open FsUnit

[<TestFixture>]
type BenchmarkCases() =

    let evaluate evalFun n b =
        let tree = complexTree evalFun n b

        let cal =
            NegamaxAI(evaluator, searchLimit.Turn(n, searchTime.Unlimited))

        ignore ((cal :> IGameAI).DetermineAction(tree.build ()))
        let logInfo = cal.LatestLogInfo

        let treeSize = ((pown b (n + 1)) - 1) / (b - 1)
        let leafNodes = pown b n

        printfn "Searched tree of depth %i, branch width %i, %i total nodes and %i branch nodes " n b treeSize leafNodes

        printfn
            "%i calculation steps and %i end nodes evaluated in %i hours %i minutes and %i seconds %i milliseconds"
            logInfo.stepsCalculated
            logInfo.endNodesEvaluated
            logInfo.elapsedTime.Hours
            logInfo.elapsedTime.Minutes
            logInfo.elapsedTime.Seconds
            logInfo.elapsedTime.Milliseconds

        printfn
            "%i paths pruned and %i successful hash table lookups"
            cal.LatestLogInfo.prunedPaths
            cal.LatestLogInfo.successfulHashMapLookups

        logInfo

    [<Explicit>]
    [<Test>]
    member this.AllUniqueNodes() =
        let mutable nextHash = 0

        let genNextHash =
            fun () ->
                nextHash <- nextHash + 1
                nextHash

        let evalFun (d, h) = (Player.Player1, d, h, genNextHash ())
        let n, b = 7, 6

        let logInfo = evaluate evalFun n b

        let expectedStepsCalculated = ((pown b n) - 1) * b / (b - 1)
        let expectedEndNodesEvaluated = pown b n

        logInfo.stepsCalculated
        |> should equal expectedStepsCalculated

        logInfo.endNodesEvaluated
        |> should equal expectedEndNodesEvaluated

        logInfo.prunedPaths |> should equal 0
        logInfo.successfulHashMapLookups |> should equal 0

    [<Explicit>]
    [<Test>]
    member this.PathsPruned() =
        let rng = Random()

        let evalFun (d, _) =
            let playerTurn = if (d % 2 = 0) then p1 else p2
            (playerTurn, d, rng.Next() / 100, rng.Next())

        let n, b = 8, 6

        let logInfo = evaluate evalFun n b

        logInfo.stepsCalculated |> should greaterThan 0
        logInfo.endNodesEvaluated |> should greaterThan 0
        logInfo.prunedPaths |> should greaterThan 0
        logInfo.successfulHashMapLookups |> should equal 0

    [<Explicit>]
    [<Test>]
    member this.HashMapUsed() =
        let rng = Random()
        let evalFun (d, h) = (p1, d, h, rng.Next() / 100)
        let n, b = 8, 6

        let logInfo = evaluate evalFun n b

        logInfo.stepsCalculated |> should greaterThan 0
        logInfo.endNodesEvaluated |> should greaterThan 0
        logInfo.prunedPaths |> should equal 0

        logInfo.successfulHashMapLookups
        |> should greaterThan 0
