namespace UnlockedCore.Test
open UnlockedCore

open NUnit.Framework
open UnlockedCore.TestTypes

[<TestFixture>]
type TestCase () =
    let evaluator = new evaluator()
    let player1 = Player.Player1
    let player2 = Player.Player2

    let basicTree = node(player1, 0, 0, [|
        node(player2, 1, -10, 
            node(player1, 2, 20,
                 node(player2, 3, -40,[|
                      node(player1, 4, 70)
                      node(player1, 4, 80)|]
                      )
                 )
            )
        node(player2, 1, -20,
            node(player1, 2, 30,
                 node(player2, 3, -50,
                      node(player1, 4, 75)
                      )
                 )
            )
        |])
    
    let rec invertTree (t : node) =
        let otherPlayer = if(t.playerTurn = player1) then player2 else player1
        if(t.nodes.Length = 0)
        then
            node(otherPlayer, t.turnNumber, t.value)
        else
            let nodes = t.nodes |> Array.map invertTree
            node(otherPlayer, t.turnNumber, t.value, nodes)

    [<Test>]
    member this.NoDepth_BasicTree () =
        let result = AIMethods.minimaxAI evaluator 0 basicTree
        
        Assert.That(fst result, Is.EqualTo(0))
        Assert.That(snd result, Is.EqualTo([||]))

    [<TestCase(1, -10, [|0|])>]
    [<TestCase(2, 30, [|1; 0|])>]
    [<TestCase(3, -40, [|0; 0; 0|])>]
    [<TestCase(4, 75, [|1; 0; 0; 0|])>]
    member this.VariousDepth_BasicTree (depth : int) (expectedValue : int) (expectedPath : int[]) =
        let result = AIMethods.minimaxAI evaluator depth basicTree
        
        Assert.That(fst result, Is.EqualTo(expectedValue))
        Assert.That(snd result, Is.EqualTo(List.ofArray expectedPath))

    [<TestCase(1, -20, [|1|])>]
    [<TestCase(2, 20, [|0; 0|])>]
    [<TestCase(3, -50, [|1; 0; 0|])>]
    [<TestCase(4, 75, [|1; 0; 0; 0|])>]
    member this.VariousDepth_InvertedBasicTree (depth : int) (expectedValue : int) (expectedPath : int[]) =
        let invertedTree = invertTree basicTree
        let result = AIMethods.minimaxAI evaluator depth invertedTree
        
        Assert.That(fst result, Is.EqualTo(expectedValue))
        Assert.That(snd result, Is.EqualTo(List.ofArray expectedPath))