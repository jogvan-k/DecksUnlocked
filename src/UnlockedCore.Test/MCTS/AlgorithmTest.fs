module UnlockedCoreTest.MCTS.AlgorithmTest

open System
open NUnit.Framework
open UnlockedCore
open UnlockedCore.MCTS.Types
open UnlockedCore.MCTS.Algorithm
open UnlockedCoreTest.TestTypes

[<TestFixture>]
type selectionTests() =
    let branchingNode = nb(p1, 0, 0, 0)
                              .addChildren([nb(p2, 0, 0, 1, nb(p2, 0, 0, 4, nb(p2, 0, 0, 7)));
                                            nb(p1, 0, 0, 2, nb(p1, 0, 0, 5, nb(p2, 0, 0, 8)))
                                            nb(p2, 0, 0, 3, nb(p2, 0, 0, 6, nb(p2, 0, 0, 9)))])
                              .build()
    
    [<Test>]
    member this.TerminalNode ([<Values(Player.Player1, Player.Player2)>] playerTurn) =
        let terminalNode = State(Parent.None, node(playerTurn, 0, 0, 0))
        let result = selection(terminalNode, leafEvaluator)
        match result with
        | Exhausted(path) -> Assert.That(path, Is.Empty)
        | _ -> Assert.Fail()
        
    [<Test>]
    member this.AllUnexploredLeaves_SelectsFirst () =
        let root = State(Parent.None, branchingNode)
        let result = selection(root, leafEvaluator)
        match result with 
        | Candidate(s, i) ->
            Assert.That((s.state :> Object).GetHashCode(), Is.EqualTo(0))
            Assert.That(i, Is.EqualTo(0))
        | _ -> Assert.Fail()
        
    [<Test>]
    member this.ExploredAndUnexploredLeaves_SelectUnexplored () =
        let root = State(Parent.None, branchingNode)
        root.leaves <- [|Leaf.Leaf(State(Parent.Parent(root), branchingNode.children.[0]));
                         Leaf.Terminal(false)
                         Leaf.Unexplored(root.state.Actions().[2])|]
        let result = selection(root, leafEvaluator)
        match result with
        | Candidate(s, i) ->
            Assert.That((s.state :> Object).GetHashCode(), Is.EqualTo(0))
            Assert.That(i, Is.EqualTo(2))
        | _ -> Assert.Fail()

    [<Test>]
    member this.AllExploredOnRoot_SelectHighestEvaluated ([<Range(1, 3)>] highestEvaluated :int) =
        let root = State(Parent.None, branchingNode)
        root.leaves <- (branchingNode :> ICoreState).Actions()
                       |> Array.mapi (fun i -> fun _ -> Leaf.Leaf(State(Parent.Parent(root), branchingNode.children.[i])))
        
        let leafEvaluator = fun i ->
            match i with
            | Leaf s -> if((s.state :> Object).GetHashCode() = highestEvaluated) then 0.1 else 0.
            | _ -> 1.
        
        let result = selection(root, leafEvaluator)
        match result with
        | Candidate(s, i) ->
            Assert.That((s.state :> Object).GetHashCode(), Is.EqualTo(highestEvaluated))
            Assert.That(i, Is.EqualTo(0))
        | _ -> Assert.Fail()
        
    [<TestCase(1, 4)>]
    [<TestCase(2, 5)>]
    [<TestCase(3, 6)>]
    member this.AllExploredOnRootAndOneLevelDown_CandidateRecursivelyChosen(highestEvaluated :int, expectedCandidate: int) =
        let root = State(Parent.None, branchingNode)
        root.leaves <- (branchingNode :> ICoreState).Actions()
                       |> Array.mapi (fun i -> fun _ -> Leaf.Leaf(State(Parent.Parent(root), branchingNode.children.[i])))
        let mutable leaves = List.empty
        for l in (branchingNode :> ICoreState).Actions() do
            let state = l.DoCoreAction()
            let leafState = State(Parent(root), state)
            leafState.leaves <- Array.singleton(Leaf(State(Parent(leafState), state.Actions().[0].DoCoreAction())))
            let leaf = Leaf(leafState)
            leaves <- leaf :: leaves
        root.leaves <- leaves |> List.rev |> List.toArray
        
        let leafEvaluator = fun i ->
            match i with
            | Leaf s -> if((s.state :> Object).GetHashCode() = highestEvaluated) then 0.1 else 0.
            | _ -> 1.
        
        let result = selection(root, leafEvaluator)
        match result with
        | Candidate(s, i) ->
            Assert.That((s.state :> Object).GetHashCode(), Is.EqualTo(expectedCandidate))
            Assert.That(i, Is.EqualTo(0))
        | _ -> Assert.Fail()