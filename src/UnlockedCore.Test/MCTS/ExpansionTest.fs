module UnlockedCoreTest.MCTS.ExpansionTest

open System
open System.Collections.Generic
open NUnit.Framework

open FsUnit
open UnlockedCoreTest.TestTypes
open UnlockedCore.MCTS.Types
open UnlockedCore.MCTS.Algorithm

[<TestFixture>]
type ExpansionTest() =
    let branchingNode = nb(p1, 0, 0, 0)
                               .addChildren([ nb (p2, 0, 0, 1, nb(p2, 0, 0, 4))
                                              nb (p1, 0, 0, 2, nb(p1, 0, 0, 5))
                                              nb (p2, 0, 0, 3, nb(p2, 0, 0, 6)) ])
                               .build()

    let constructSut() = State(branchingNode)

    let stateHash (s: State) = (s.state :> Object).GetHashCode()

    let assertIsState leaf expandTo =
        match leaf with
        | Leaf.Leaf a -> stateHash a.state |> should equal expandTo
        | _ -> Assert.Fail()
        
    let assertIsTerminal terminal win =
        match terminal with
        | Terminal w -> w |> should equal win
        | _ -> Assert.Fail()
        
    [<Test>]
    member this.ExpandUnexplored([<Range(1, 3)>] expandTo) =
        let sut = constructSut()
        let result = expansion (sut, expandTo - 1, Option.None)
        assertIsState result expandTo
        sut.leaves.[expandTo - 1] |> should be (ofCase<@ Leaf.Leaf @>)
        
    [<Test>]
    member this.ExpandToTerminal() =
        let node = nb(p1, 0, 0, 0, nb(p2, 0, 0, 2)).build()
        let sut = State(node)
        let result = expansion ( sut, 0, Option.None)
        assertIsTerminal result p2
        sut.leaves.[0] |> should be (ofCase<@ Terminal @>)
        
    [<Test>]
    member this.ExpandExplored() =
        let sut = constructSut()
        sut.leaves.[0] <- Leaf(Action(p1, sut))

        (fun () -> expansion (sut, 0, Option.None) |> ignore)
        |> should (throwWithMessage "Target leaf is already expanded") typeof<Exception>
    
    [<Test>]
    [<Ignore("TODO implement transposition tables")>]
    member this.ExpandWithTranspositionTable([<Range(1, 3)>] expandTo) =
        let sut = constructSut()
        let tTable = HashSet<int>([0]) :> ISet<int>
        let result = expansion (sut, expandTo - 1, Some(tTable))
        assertIsState result expandTo
        sut.leaves.[expandTo - 1] |> should be (ofCase<@ Leaf.Leaf @>)
        tTable |> should equivalent [0;expandTo]
        
    [<Test>]
    [<Ignore("TODO implement transposition tables")>]
    member this.ExpandToValueInTranspositionTable([<Range(1, 3)>] expandTo) =
        let sut = constructSut()
        let tTable = HashSet<int>([0;expandTo]) :> ISet<int>
        let result = expansion (sut, expandTo - 1, Some(tTable))
        result |> should be Null
        sut.leaves.[expandTo - 1] |> should be (ofCase<@ Duplicate @>)
        tTable |> should equivalent [0;expandTo]