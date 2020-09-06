namespace UnlockedCore.Test
open AIMethods

open NUnit.Framework
open UnlockedCore.TestTypes

[<TestFixture>]
type TestCase () =

    let basicTree = node(p1, 0, 0, 0, [|
        node(p2, 1, -10, 1, 
            node(p1, 2, 20, 2,
                 node(p2, 3, -40, 3,[|
                      node(p1, 4, 70, 4)
                      node(p1, 4, 80, 5)|]
                      )
                 )
            )
        node(p2, 1, -20, 6,
            node(p1, 2, 30, 7,
                 node(p2, 3, -50, 8,
                      node(p1, 4, 75, 9)
                      )
                 )
            )
        |])
    
    let twoDepthsPerTurnTree = node(p1, 0, 0, 0, [|
        node(p1, 0, 20, 1, [|
            node(p2, 1, 20, 2, [|
                node(p2, 1, 0, 3, [|
                    node(p1, 2, -10, 4)
                |])
            |])
            node(p2, 1, 25, 5, [|
                node(p2, 1, 20, 6, [|
                    node(p1, 2, -20, 7,[|
                         node(p1, 2, 0, 8, [|
                             node(p2, 3, 30, 9)
                         |])
                    |])
                |])
            |])
        |])
        node(p1, 0, 10, 10)
    |])
    
    [<Test>]
    member this.NoDepth_BasicTree () =
        let d = Depth(0)
        let result = minimaxAI (accumulator(evaluatorFunc, LoggingConfiguration.NoLogging)) d basicTree
        
        Assert.That(fst result, Is.EqualTo(0))
        Assert.That(snd result, Is.EqualTo([||]))

    [<TestCase(1, -10, [|0|])>]
    [<TestCase(2, 30, [|1; 0|])>]
    [<TestCase(3, -40, [|0; 0; 0|])>]
    [<TestCase(4, 75, [|1; 0; 0; 0|])>]
    member this.VariousDepth_BasicTree (depth : int) (expectedValue : int) (expectedPath : int[]) =
        let d = Depth(depth)
        let result = minimaxAI (accumulator(evaluatorFunc, LoggingConfiguration.NoLogging)) d basicTree
        
        Assert.That(fst result, Is.EqualTo(expectedValue))
        Assert.That(snd result, Is.EqualTo(List.ofArray expectedPath))

    [<TestCase(1, -20, [|1|])>]
    [<TestCase(2, 20, [|0; 0|])>]
    [<TestCase(3, -50, [|1; 0; 0|])>]
    [<TestCase(4, 75, [|1; 0; 0; 0|])>]
    member this.VariousDepth_InvertedBasicTree (depth : int) (expectedValue : int) (expectedPath : int[]) =
        let invertedTree = invertTree basicTree
        let d = Depth(depth)
        let result = minimaxAI (accumulator(evaluatorFunc, LoggingConfiguration.NoLogging)) d invertedTree
        
        Assert.That(fst result, Is.EqualTo(expectedValue))
        Assert.That(snd result, Is.EqualTo(List.ofArray expectedPath))

    [<TestCase(1, 25, [|0; 1|])>]
    [<TestCase(2, 10, [|1|])>]
    [<TestCase(3, 30, [|0; 1; 0; 0; 0; 0|])>]
    member this.TurnDepthSearch (untilTurn : int) (expectedValue : int) (expectedPath : int[]) =
        let d = Until(untilTurn, 0)
        
        let result = minimaxAI (accumulator(evaluatorFunc, LoggingConfiguration.NoLogging)) d twoDepthsPerTurnTree
        
        Assert.That(fst result, Is.EqualTo(expectedValue))
        Assert.That(snd result, Is.EqualTo(expectedPath))