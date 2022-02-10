module UnlockedCore.MCTS.Types

open System
open UnlockedCore

type Leaf =
    | Unexplored of ICoreAction
    | Leaf of Action
    | Terminal of Player

and State(state: ICoreState) =
    let mutable _leaves = state.Actions() |> Array.map Unexplored
    let mutable _visitCount = 0
    let mutable _winRate = 0.
    let incrementVisitCount () = _visitCount <- _visitCount + 1
    member this.state = state

    member this.leaves
        with get () = _leaves
        and set (value) = _leaves <- value

    member this.registerWin() =
        incrementVisitCount ()
        _winRate <- _winRate + (1. - _winRate) / float (_visitCount)

    member this.registerLoss() =
        incrementVisitCount ()
        _winRate <- _winRate - _winRate / float (_visitCount)

    member this.winRate = _winRate
    member this.visitCount = Math.Max(1, _visitCount)
    member this.playerTurn = state.PlayerTurn

and Action(activePlayer: Player, state: State) =
    let mutable _visitCount = 0
    member this.incrementVisitCount() = _visitCount <- _visitCount + 1
    member this.state = state
    member this.visitCount = Math.Max(_visitCount, 1)

    member this.winRate =
        if state.state.PlayerTurn = activePlayer then
            state.winRate
        else
            1. - state.winRate

type SelectionResult =
    | Exhausted of (Action list * Player)
    | Candidate of (Action list * int)

type TranspositionTable() =
    let mutable _map = Map.empty
    let mutable _successfulLookups = 0

    member this.Add(h: int, s: State) = _map <- _map.Add(h, s)

    member this.Lookup h =
        let result = _map.TryFind h

        if result.IsSome then
            _successfulLookups <- _successfulLookups + 1

        result

    member this.SuccessfulLookups = _successfulLookups
    member this.Count = _map.Count

type LogInfo =
    struct
        val mutable simulations: int
        val mutable elapsedTime: TimeSpan
        val mutable estimatedAiWinChance: float
        val mutable successfulTranspositionTableLookup: int
        val mutable transpositionTableSize: int
    end
