module UnlockedCore.Algorithms.Negamax

open System
open UnlockedCore
open UnlockedCore.Algorithms.AISupportTypes
open UnlockedCore.Algorithms.Accumulator
open UnlockedCore.Algorithms.TranspositionTable
open UnlockedCore.Algorithms.UtilityMethods

let rec recNegamax alpha beta color (depth: searchLimit) (s: ICoreState) (acc: accumulator) (tTable: ITranspositionTable)=
    match s with
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
            for a in actions do
                if(alpha' < beta)
                then let candidate = nextCandidate alpha' beta color ((fst a).DoCoreAction()) depth acc tTable
                     if (fst candidate) > (fst currentBest)
                     then currentBest <- (fst candidate, (snd a) :: snd candidate)
                          alpha' <- max alpha' (fst candidate)
                else acc.incrementPrunedPaths()

            tTable.add stateHash currentBest
            currentBest
and nextCandidate alpha beta color (state: ICoreState) depth acc tTable =
    let nextDepth = reduceSearchLimit depth
    if (changingPlayer state)
    then
        recNegamax (-1 * beta) (-1 * alpha) (-color) nextDepth state acc tTable |> flipValue
    else
        recNegamax alpha beta color depth state acc tTable

let negamax (depth: searchLimit) (s: ICoreState) (acc: accumulator) =
    let transpositionTable = new transpositionTable(acc.logConfig, acc)
    let result = recNegamax -Int32.MaxValue Int32.MaxValue (startColor s) depth s acc transpositionTable
    acc.successfulHashMapLookups <- (transpositionTable :> ITranspositionTable).successfulLookups
    result