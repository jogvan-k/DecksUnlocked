namespace UnlockedCore

open System

type Player =
    | Player1 = 1
    | Player2 = 2

type ICoreState =
    abstract PlayerTurn: Player
    abstract TurnNumber: int
    abstract Actions: unit -> ICoreAction []
    abstract PreviousState: ICoreState Option

and ICoreAction =
    abstract Origin: ICoreState
    abstract DoCoreAction: unit -> ICoreState
    inherit IComparable

type IEvaluator =
    abstract Evaluate: ICoreState -> int

type IGameAI =
    abstract DetermineAction: ICoreState -> int[]
    
type searchLimit =
    | Until of turn: int * depth: int
    | Depth of remaining: int
    
type SearchConfiguration =
    | NoRestrictions = 0
    | NoHashTable = 1
    
[<Flags>]
type LoggingConfiguration =
    | NoLogging                    = 0x0
    | LogEvaluatedStates           = 0x1
    | LogTime                      = 0x2
    | LogSuccessfulHashMapLookup   = 0x4
    | LogPrunedPaths               = 0x8
    | LogAll                       = 0xf