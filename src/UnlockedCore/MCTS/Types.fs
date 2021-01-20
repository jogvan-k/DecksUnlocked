module UnlockedCore.MCTS.Types

open System
open UnlockedCore

type Parent =
    | None
    | Parent of State

and Leaf =
    | Unexplored of ICoreAction
    | Leaf of State
    | Terminal of bool

and State(parent: Parent, state: ICoreState) =
    let mutable _leaves = state.Actions() |> Array.map Unexplored
    let mutable _visitCount = 0
    let mutable _winRate = 0.
    member this.parent = parent
    member this.state = state
    member this.leaves
        with get() = _leaves
        and set(value) = _leaves <- value
    member this.incrementVisits() =
        _visitCount <- _visitCount + 1
    member this.registerWin() =
        this.incrementVisits()
        _winRate <- _winRate + (1. - _winRate)/float(_visitCount)
    member this.registerLoss() =
        this.incrementVisits()
        _winRate <- _winRate - _winRate/float(_visitCount)
    member this.winRate
        with get() = _winRate
    member this.visitCount
        with get() = System.Math.Max(1, _visitCount)

type SelectionResult =
    | Exhausted of State
    | Candidate of State * int
    
type LogInfo =
    struct
        val mutable simulations: int
        val mutable elapsedTime: TimeSpan
        val mutable estimatedAiWinChance: float
    end