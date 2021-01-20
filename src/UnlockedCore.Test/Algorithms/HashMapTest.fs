module UnlockedCore.Test.HashMapTest

open NUnit.Framework
open UnlockedCore
open UnlockedCore.AI.MinimaxTypes
open UnlockedCore.AITypes
open UnlockedCoreTest.TestTypes

open FsUnit

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
        let sut = NegamaxAI(evaluator, searchLimit.Turn(4, searchTime.Unlimited))
        
        let path = (sut :> IGameAI).DetermineAction tree
        
        path |> should equal [0;0]
        sut.LatestLogInfo.successfulHashMapLookups |> should equal 2