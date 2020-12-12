module UnlockedCore.Algorithms.Negamax

open System
open UnlockedCore
open UnlockedCore.Algorithms.AISupportTypes
open UnlockedCore.Algorithms.Accumulator
open UnlockedCore.Algorithms.UtilityMethods

let rec recNegamax alpha beta color (depth: searchLimit) (s: ICoreState) (acc: accumulator) =
    match s with
    | SearchLimit depth _
    | Terminal _ -> (color * acc.eval s, [])
    | Node (actions) ->
        let stateHash = s.GetHashCode()
        let hashMapLookup = acc.lookupHashMap stateHash
        if(hashMapLookup.IsSome)
        then hashMapLookup.Value
        else
            let iter = actions.GetEnumerator()
            let mutable alpha' = alpha
            let mutable currentBest = (Int32.MinValue, [])
            while iter.MoveNext() do
                let action, i = iter.Current
                if(alpha' < beta)
                then let candidate = nextCandidate recNegamax alpha' beta color (action.DoCoreAction()) depth acc
                     if (fst candidate) > (fst currentBest)
                     then currentBest <- (fst candidate, i :: snd candidate)
                          alpha' <- max alpha' (fst candidate)
                else acc.incrementPrunedPaths()

            acc.addToHashMap stateHash currentBest
            currentBest



let negamax (depth: searchLimit) (s: ICoreState) =
    recNegamax -Int32.MaxValue Int32.MaxValue (startColor s) depth s
    
//let incrementalNegamax (depth: searchLimit) (s: ICoreState) =
