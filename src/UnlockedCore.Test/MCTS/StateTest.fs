module UnlockedCoreTest.MCTS.StateTest

open NUnit.Framework
open UnlockedCore.MCTS.Types
open UnlockedCoreTest.TestTypes
open FsUnit

[<TestFixture>]
type StateTest() =
    
    let sampleState() =
        State(Parent.None, node(p1, 0, 0, 0))
        
    [<Test>]
    member this.OnlyWin()=
        let state = sampleState()
        
        for _ in [1..10] do
            state.registerWin()
        
        state.visitCount |> should equal 10
        state.winRate |> should equal 1.
    
    [<Test>]
    member this.OnlyLoss() =
        let state = sampleState()
        
        for _ in [1..10] do
            state.registerLoss()
        
        state.visitCount |> should equal 10
        state.winRate |> should equal 0.
    
    [<Test>]
    member this.WinThenLosses() =
        let state = sampleState()
        
        state.registerWin()
        for _ in [1..99] do
            state.registerLoss()
        
        state.visitCount |> should equal 100
        state.winRate |> should (equalWithin 0.0000001) 0.01