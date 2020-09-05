namespace UnlockedCore.Test.BenchmarkTest

open UnlockedCore.AITypes
open NUnit.Framework
open UnlockedCore
open UnlockedCore.TestTypes

[<TestFixture>]
type BenchmarkCases () =
    
    //let stringifyResult result = (fst result, (snd result) |> List.fold (fun r s -> r + s + ","))
    
    [<Explicit>]
    [<Test>]
    member this.AllUniqueNodes () =
        let evalFun (d,h) = (Player.Player1, d, h)
        let n = 8
        let b = 6
        let tree = complexTree evalFun n b
        
        
        let cal = MinimaxAI(evaluator, n, SearchDepthConfiguration.turn, AIMethods.LoggingConfiguration.LogAll)
        
        let result = (cal :> IGameAI).DetermineAction tree
        let logInfo = cal.logInfo
        
        let treeSize = ((pown b (n + 1)) - 1) / (b - 1)
        let leafNodes = pown b n
        
        printfn "Searched tree of depth %i, branch width %i, %i total nodes and %i branch nodes " n b treeSize leafNodes
        printfn "%i nodes evaluated in %i hours %i minutes and %i seconds %i milliseconds" logInfo.NodesEvaluated logInfo.ElapsedTime.Hours logInfo.ElapsedTime.Minutes logInfo.ElapsedTime.Seconds logInfo.ElapsedTime.Milliseconds
        //printfn "Guaranteed minimum score of %i by following the path %s" (fst result) (snd result)