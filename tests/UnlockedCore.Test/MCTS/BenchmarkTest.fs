module UnlockedCoreTest.MCTS.BenchmarkTest

open NUnit.Framework

open UnlockedCore
open UnlockedCore.MCTS.AI
open UnlockedCoreTest.TestTypes

let unlimited = 999999999

[<TestFixture>]
type BenchmarkCases() =

    let evaluate evalFun n b =
        let tree = complexTree evalFun n b

        let cal =
            MonteCarloTreeSearch(Seconds(5), unlimited, configuration.AsyncExecution)

        let bestPath =
            (cal :> IGameAI).DetermineAction(tree.build ())

        let logInfo = cal.LatestLogInfo()

        let treeSize = ((pown b (n + 1)) - 1) / (b - 1)
        let leafNodes = pown b n

        printfn "Searched tree of depth %i, branch width %i, %i total nodes and %i branch nodes " n b treeSize leafNodes

        printfn
            "%i simulations performed in %i hours %i minutes and %i seconds %i milliseconds"
            logInfo.simulations
            logInfo.elapsedTime.Hours
            logInfo.elapsedTime.Minutes
            logInfo.elapsedTime.Seconds
            logInfo.elapsedTime.Milliseconds

        printfn
            "Best path: %s"
            (bestPath
             |> Array.map (string)
             |> Array.fold (fun s i -> if (s = "") then i else s + ", " + i) "")

    [<Explicit>]
    [<Test>]
    member this.AllUniqueNodes() =
        let mutable nextHash = 0

        let genNextHash =
            fun () ->
                nextHash <- nextHash + 1
                nextHash

        let n, b = 10, 2

        let totalEndNodes = (int32) (2. ** 10.)

        let evalFun (d, h) =
            let playerTurn =
                if d = n then
                    if h > totalEndNodes / 4 then p1 else p2
                else if d % 2 = 1 then
                    p2
                else
                    p1

            (playerTurn, d, 0, genNextHash ())

        evaluate evalFun n b
