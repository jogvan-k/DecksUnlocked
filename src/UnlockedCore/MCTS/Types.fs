module UnlockedCore.MCTS.Types

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
    let _visitCount = 0
    let _wins = 0
    member this.parent = parent
    member this.state = state
    member this.leaves
        with get() = _leaves
        and set(value) = _leaves <- value

type SelectionResult =
    | Exhausted of int[]
    | Candidate of State * int