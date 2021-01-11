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
    abstract DetermineAction: state: ICoreState -> int[]
    // Calculates the best path for a given state. A previously calculated best path can be provided from previous calculations to speed up
    // calculation time.
    abstract DetermineActionWithVariation: state: (ICoreState ) -> variation: int[] -> int[]

type searchTime =
    | Unlimited
    | Minutes of int
    | Seconds of int
    | MilliSeconds of int

type searchLimit =
    | Ply of plies: int * searchTime: searchTime
    | Turn of turns: int * searchTime: searchTime

type SearchDepthConfiguration =
    | actions = 0
    | turn = 1

type SearchConfiguration =
    | NoRestrictions    = 0x0
    | NoHashTable       = 0x1
    | IncrementalSearch = 0x2
    
[<Flags>]
type LoggingConfiguration =
    | NoLogging                    = 0x00
    | LogEvaluatedEndStates        = 0x01
    | LogCalculatedSteps           = 0x02
    | LogTime                      = 0x04
    | LogSuccessfulHashMapLookup   = 0x08
    | LogPrunedPaths               = 0x10
    | LogAll                       = 0x1f

type LogInfo =
    struct
        val mutable endNodesEvaluated: int
        val mutable stepsCalculated: int
        val mutable elapsedTime: TimeSpan
        val mutable prunedPaths: int
        val mutable successfulHashMapLookups: int
    end