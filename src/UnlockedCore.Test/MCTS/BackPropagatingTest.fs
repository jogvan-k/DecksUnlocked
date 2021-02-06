module UnlockedCoreTest.MCTS.BackPropagatingTest

open UnlockedCore
open UnlockedCore.MCTS.Algorithm
open UnlockedCore.MCTS.Types

open UnlockedCoreTest.TestTypes

open NUnit.Framework

open FsUnit

[<TestFixture>]
type BackPropagatingTest() =
    
    let branchingNode =
        nb(p1, 0, 0, 0,
           [nb(p1, 1, 0, 1).addChild(nb(p2, 2, 0, 11)).addChild(nb(p1, 3, 0, 111))
            nb(p2, 1, 0, 2).addChild(nb(p1, 2, 0, 22)).addChild(nb(p2, 3, 0, 222))
            nb(p1, 1, 0, 3).addChild(nb(p2, 2, 0, 33)).addChild(nb(p1, 3, 0, 333))])
            
    let constructSut(nodes: ICoreState) =
        let root = State(Parent.None, nodes)
        root.leaves <- [|0; 1; 2|]
        |> Array.map (fun i ->
            let childNode = nodes.Actions().[i].DoCoreAction()
            let s1 = State(Parent.Parent(root), childNode)
            let grandchild = childNode.Actions().[0].DoCoreAction()
            let s2 = State(Parent.Parent(s1), grandchild)
            s1.leaves.[0] <- Leaf.Leaf(s2)
            Leaf.Leaf(s1))
        root
        
    let getLeaf l =
        match l with
        | Leaf.Leaf v -> v
        | _ -> failwith "not a leaf"
    
    let assertWinRate (state: State) win =
           state.winRate |> should equal (if win then 1. else 0.)
    
    [<Test>]
    member this.BranchingTree([<Values(0, 1, 2)>] branch, [<Values(true, false)>] win) =
        let root = constructSut(branchingNode.build())
        let endNode = getLeaf( getLeaf(root.leaves.[branch]).leaves.[0])
        
        backPropagating(endNode) win
        
        root.visitCount |> should equal 1
        root.winRate |> should equal (if win then 1. else 0.)
        
        let visitedBranch = getLeaf root.leaves.[branch]
        
        assertWinRate visitedBranch win
        let visitedChildBranch = getLeaf(visitedBranch.leaves.[0])
        assertWinRate visitedChildBranch win
        
        for i in [|0;1;2|] |> Array.where(fun i -> i <> branch) do
            let leaf = getLeaf root.leaves.[i]
            leaf.winRate |> should equal 0.
            leaf.visitCount |> should equal 1.