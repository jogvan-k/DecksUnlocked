namespace UnlockedCore

open System

type Player =
    | Player1 = 1
    | Player2 = 2

type ICoreState =
    abstract PlayerTurn: Player
    abstract TurnNumber: int
    abstract Actions: unit -> ICoreAction []

and ICoreAction =
    abstract Origin: ICoreState
    abstract DoCoreAction: unit -> ICoreState

type IEvaluator =
    abstract Evaluate: ICoreState -> int

type IGameAI =
    abstract DetermineAction: ICoreState -> int[]