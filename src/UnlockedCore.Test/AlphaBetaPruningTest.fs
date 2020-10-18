module UnlockedCore.Test.AlphaBetaPruningTest

open NUnit.Framework
open UnlockedCore
open UnlockedCore.AITypes
open UnlockedCore.TestTypes

[<TestFixture>]
type alphaBetaPruningTest() =

    let leaf no =
        node (p2, 1, 0, 1 * no, [|
            node(p1, 2, 10, 10 * no, [||])
            node(p1, 2, 30, 100 * no, [||])
            node(p1, 2, 20, 1000 * no, [||])
        |])
    
    let alphaBetaPruningTree = node(p1, 0, 0, 0, [|leaf 1; leaf 2; leaf 3|])

    [<Test>]
    member this.PruningOnMaximizingPlayer () =
        let sut = MinimaxAI(evaluator, 4, SearchDepthConfiguration.turn, AIMethods.LoggingConfiguration.LogEvaluatedStates)
        
        let path = (sut :> IGameAI).DetermineAction alphaBetaPruningTree
        
        
        Assert.That([|0; 0|], Is.EqualTo(Array.toList path))
        
        Assert.AreEqual(5, sut.LatestLogInfo.nodesEvaluated)
        
    [<Test>]
    member this.PruningOnMinimizingPlayer () =
        let sut = MinimaxAI(evaluator, 4, SearchDepthConfiguration.turn, AIMethods.LoggingConfiguration.LogEvaluatedStates)
        
        let invertedTree = invertTree alphaBetaPruningTree
        let path = (sut :> IGameAI).DetermineAction invertedTree
        
        Assert.That([|0; 1|], Is.EqualTo(Array.toList path))
        
        Assert.AreEqual(7, sut.LatestLogInfo.nodesEvaluated)