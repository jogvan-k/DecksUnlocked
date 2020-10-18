module UnlockedCore.Test.HashMapTest

open NUnit.Framework
open UnlockedCore
open UnlockedCore.AITypes
open UnlockedCore.TestTypes

[<TestFixture>]
type hashMapTest() =
    
    let tree =
        node(p1, 0, 0, 1234, [|
            node(p2, 1, 0, 4321, [|
                node(p1, 2, 100, 1111)
            |]);
            node(p2, 1, 0, 4321, [|
                node(p1, 2, 100, 1111)
            |]);
            node(p2, 1, 0, 4321, [|
                node(p1, 2, 100, 1111);
            |])
        |])
    
    [<Test>]
    member this.SearchWithHashTableLookup () =
        let sut = MinimaxAI(evaluator, 4, SearchDepthConfiguration.turn, AIMethods.LoggingConfiguration.LogAll)
        
        let path = (sut :> IGameAI).DetermineAction tree
        
        Assert.That(path, Is.EqualTo(Seq.toArray [|0; 0|]))
        Assert.That(sut.LatestLogInfo.successfulHashMapLookups, Is.EqualTo(2))