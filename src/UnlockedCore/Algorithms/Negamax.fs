module UnlockedCore.Algorithms.Negamax

open System
open UnlockedCore
open UnlockedCore.Algorithms.AISupportTypes
open UnlockedCore.Algorithms.Accumulator
open UnlockedCore.Algorithms.TranspositionTable
open UnlockedCore.Algorithms.UtilityMethods

let rec recNegamax alpha beta color (depth: remainingSearch) (s: ICoreState) (acc: accumulator) (tTable: ITranspositionTable) pv =
    match (s, pv) with
    | SearchLimit depth _
    | Terminal _ -> (color * acc.eval s, [])
    | Node (actions) ->
        let stateHash = s.GetHashCode()
        let hashMapLookup = tTable.lookup stateHash
        if(hashMapLookup.IsSome)
        then hashMapLookup.Value
        else
            let mutable alpha' = alpha
            let mutable currentBest = (Int32.MinValue, [])
            let iter = actions.GetEnumerator()
            while(iter.MoveNext() && not acc.isTimeLimitReached) do
                let (a, i, p) = iter.Current
                if(alpha' < beta)
                then let candidate = nextCandidate alpha' beta color (a.DoCoreAction()) depth acc tTable p
                     if (fst candidate) > (fst currentBest)
                     then currentBest <- (fst candidate, i :: snd candidate)
                          alpha' <- max alpha' (fst candidate)
                else acc.incrementPrunedPaths()

            tTable.add stateHash currentBest
            currentBest
and nextCandidate alpha beta color (state: ICoreState) depth acc tTable pv =
    let nextDepth = reduceRemainingSearch depth
    if (changingPlayer state)
    then
        recNegamax (-1 * beta) (-1 * alpha) (-color) nextDepth state acc tTable pv |> flipValue
    else
        recNegamax alpha beta color nextDepth state acc tTable pv

let negamax (depth: remainingSearch) (s: ICoreState) (acc: accumulator) pv =
    acc.startTimer
    let transpositionTable = new transpositionTable(acc.logConfig, acc)
    let result = recNegamax -Int32.MaxValue Int32.MaxValue (startColor s) depth s acc transpositionTable pv
    acc.successfulHashMapLookups <- (transpositionTable :> ITranspositionTable).successfulLookups
    result