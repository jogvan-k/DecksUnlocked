module UnlockedCore.Test.AlphaBetaPruningTest

open NUnit.Framework
open UnlockedCore
open UnlockedCore.AITypes
open UnlockedCore.TestTypes

[<TestFixture>]
type alphaBetaPruningTest() =

    let leaf =
        node (p2, 1, 0, [|
            node(p1, 2, 10, [||])
            node(p1, 2, 30, [||])
            node(p1, 2, 20, [||])
        |])
    
    let alphaBetaPruningTree = node(p1, 0, 0, [|leaf; leaf; leaf|])

    [<Test>]
    member this.PruningOnMaximizingPlayer () =
        let sut = MinimaxAI(evaluator, 4, SearchDepthConfiguration.turn, AIMethods.LoggingConfiguration.LogEvaluatedStates)
        
        let path = (sut :> IGameAI).DetermineAction alphaBetaPruningTree
        
        
        Assert.That([|0; 0|], Is.EqualTo(Array.toList path))
        
        Assert.AreEqual(5, sut.logInfo.NodesEvaluated)
        
    [<Test>]
    member this.PruningOnMinimizingPlayer () =
        let sut = MinimaxAI(evaluator, 4, SearchDepthConfiguration.turn, AIMethods.LoggingConfiguration.LogEvaluatedStates)
        
        let invertedTree = invertTree alphaBetaPruningTree
        let path = (sut :> IGameAI).DetermineAction invertedTree
        
        Assert.That([|0; 1|], Is.EqualTo(Array.toList path))
        
        Assert.AreEqual(7, sut.logInfo.NodesEvaluated)