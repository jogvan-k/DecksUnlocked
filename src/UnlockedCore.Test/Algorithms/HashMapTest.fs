module UnlockedCore.Test.HashMapTest

open NUnit.Framework
open UnlockedCore
open UnlockedCore.AITypes
open UnlockedCoreTest.TestTypes

[<TestFixture>]
type hashMapTest() =
    
    let tree = nb(p1, 0, 0, 1234)
                    .addChildren([
                        nb(p2, 1, 0, 4321, [nb(p1, 2, 100, 1111)])
                        nb(p2, 1, 0, 4321, [nb(p1, 2, 100, 1111)])
                        nb(p2, 1, 0, 4321, [nb(p1, 2, 100, 1111)])
                    ])
                    .build()
    
    [<Test>]
    member this.SearchWithHashTableLookup () =
        let sut = NegamaxAI(evaluator, 4, SearchDepthConfiguration.turn)
        
        let path = (sut :> IGameAI).DetermineAction tree
        
        Assert.That(path, Is.EqualTo(Seq.toArray [|0; 0|]))
        Assert.That(sut.LatestLogInfo.successfulHashMapLookups, Is.EqualTo(2))