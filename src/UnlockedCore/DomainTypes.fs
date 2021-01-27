namespace UnlockedCore

open System

type Player =
    | None = 0
    | Player1 = 1
    | Player2 = 2

type ICoreState =
    abstract PlayerTurn: Player
    abstract TurnNumber: int
    abstract Actions: unit -> ICoreAction []

and ICoreAction =
    abstract Origin: ICoreState
    abstract DoCoreAction: unit -> ICoreState
    inherit IComparable

type IEvaluator =
    abstract Evaluate: ICoreState -> int

type IGameAI =
    // Calculates the best path for a given state.
    abstract DetermineAction: state:ICoreState -> int []

type IGameAIWithVariationPath =
    // Calculates the best path for a given state. A previously calculated best path can be provided from previous calculations to speed up
    // calculation time.
    abstract DetermineActionWithVariation: state:(ICoreState) -> variation:int [] -> int []

type searchTime =
    | Unlimited
    | Minutes of int
    | Seconds of int
    | MilliSeconds of int
