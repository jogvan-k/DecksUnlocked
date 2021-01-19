module UnlockedCore.AI.MinimaxTypes

open System
open UnlockedCore

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