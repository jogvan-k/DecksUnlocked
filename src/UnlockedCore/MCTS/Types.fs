module UnlockedCore.MCTS.Types

open System
open UnlockedCore

type Leaf =
    | Unexplored of ICoreAction
    | Leaf of Action
    | Terminal of Player
    | Duplicate

and State(state: ICoreState) =
    let mutable _leaves = state.Actions() |> Array.map Unexplored
    let mutable _visitCount = 0
    let mutable _winRate = 0.
    let incrementVisitCount() =
        _visitCount <- _visitCount + 1
    member this.state = state
    member this.leaves
        with get() = _leaves
        and set(value) = _leaves <- value
    member this.registerWin() =
        incrementVisitCount()
        _winRate <- _winRate + (1. - _winRate)/float(_visitCount)
    member this.registerLoss() =
        incrementVisitCount()
        _winRate <- _winRate - _winRate/float(_visitCount)
    member this.winRate
        with get() = _winRate
    member this.visitCount
        with get() = Math.Max(1, _visitCount)
    member this.playerTurn
        with get() = state.PlayerTurn
and Action(activePlayer: Player, state: State) =
    let mutable _visitCount = 0
    member this.incrementVisitCount() =
        _visitCount <- _visitCount + 1
    member this.state with get() = state
    member this.visitCount with get() = Math.Max(_visitCount, 1)
    member this.winRate with get() =
        if state.state.PlayerTurn = activePlayer then state.winRate else 1. - state.winRate

type SelectionResult =
    | Exhausted of (Action list * Player)
    | Candidate of (Action list * int)
    
type LogInfo =
    struct
        val mutable simulations: int
        val mutable elapsedTime: TimeSpan
        val mutable estimatedAiWinChance: float
    end