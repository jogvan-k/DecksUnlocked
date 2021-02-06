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
        let root = State(nodes)
        root.leaves <- [|0; 1; 2|]
        |> Array.map (fun i ->
            let childNode = nodes.Actions().[i].DoCoreAction()
            let s1 = State(childNode)
            let grandchild = childNode.Actions().[0].DoCoreAction()
            let s2 = State(grandchild)
            s1.leaves.[0] <- Leaf.Leaf(Action(grandchild.PlayerTurn, s2))
            Leaf.Leaf(Action(root.playerTurn, s1)))
        root
        
    let getLeaf l =
        match l with
        | Leaf.Leaf a -> a.state
        | _ -> failwith "not a leaf"
    
    let assertWinRate (state: State) expectWinningPlayer =
           state.winRate |> should equal (if state.state.PlayerTurn = expectWinningPlayer then 1. else 0.)
    
    [<Test>]
    member this.BranchingTree([<Values(0, 1, 2)>] branch, [<Values(Player.Player1, Player.Player2)>] playerWin) =
        let root = constructSut(branchingNode.build())
        let state2 = getLeaf(root.leaves.[branch])
        let action1 = Action(root.playerTurn, state2)
        let action2 = Action(state2.playerTurn, getLeaf( state2.leaves.[0]))
        let visitedActions = [ action1; action2 ]
        
        backPropagating root visitedActions playerWin
        backPropagating root visitedActions playerWin
        
        root.visitCount |> should equal 2
        assertWinRate root playerWin
        
        action1.visitCount |> should equal 2
        action1.state.visitCount |> should equal 2
        assertWinRate action1.state playerWin
        
        action2.visitCount |> should equal 2
        action2.state.visitCount |> should equal 2
        assertWinRate action2.state playerWin
        
        for i in [|0;1;2|] |> Array.where(fun i -> i <> branch) do
            let leaf = getLeaf root.leaves.[i]
            leaf.winRate |> should equal 0.
            leaf.visitCount |> should equal 1.