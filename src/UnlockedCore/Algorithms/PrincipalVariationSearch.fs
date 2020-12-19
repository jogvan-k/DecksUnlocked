module UnlockedCore.Algorithms.PVS

open System
open UnlockedCore
open UnlockedCore.Algorithms.AISupportTypes
open UnlockedCore.Algorithms.Accumulator
open UnlockedCore.Algorithms.TranspositionTable
open UnlockedCore.Algorithms.UtilityMethods
open UnlockedCore.Algorithms.Negamax

let noTranspositionTable = noTranspositionTable()

let rec recPVS alpha beta color (depth: searchLimit) (s: ICoreState) (acc: accumulator) (tTable: ITranspositionTable) pv =
    match (s, pv) with
    | SearchLimit depth _
    | Terminal _ -> (color * acc.eval s, [])
    | Node (actions) ->
        let stateHash = s.GetHashCode()
        let hashMapLookup = tTable.lookup stateHash
        if(hashMapLookup.IsSome)
        then hashMapLookup.Value
        else match actions |> Seq.toList with
             | (principalAction, principalIndex, principalPath) :: tail ->
                 let doNullWindowSearch = not (Seq.isEmpty principalPath)
                 let principal = nextCandidate alpha beta color (principalAction.DoCoreAction()) depth acc tTable principalPath
                 let mutable currentBest = (fst principal, principalIndex :: snd principal)
                 let mutable alpha' = max alpha (fst principal)
                 for (action, index, _) in tail do
                     if(alpha' < beta)
                     then let mutable candidate = if(doNullWindowSearch)
                                                  then nullWindowSearch alpha' beta color (action.DoCoreAction()) depth acc tTable
                                                  else nextCandidate alpha' beta color (action.DoCoreAction()) depth acc tTable []
                          if (fst currentBest) < (fst candidate)
                          then currentBest <- (fst candidate, index :: snd candidate)
                               alpha' <- max alpha' (fst candidate)
                     else  acc.incrementPrunedPaths()
                     
                 tTable.add stateHash currentBest
                 currentBest
             | [] -> raise(SystemException("No available actions."))
and nullWindowSearch alpha beta color (state: ICoreState) depth acc tTable =
    let mutable candidate = nextCandidate alpha (alpha + 1) color state depth acc noTranspositionTable []
    if(alpha < (fst candidate) && (fst candidate) < beta)
    then candidate <- nextCandidate (fst candidate) beta color state depth acc tTable []
    candidate

and nextCandidate alpha beta color (state: ICoreState) depth acc tTable p =
    let nextDepth = reduceSearchLimit depth
    if (changingPlayer state)
    then
        recPVS (-1 * beta) (-1 * alpha) (-color) nextDepth state acc tTable p |> flipValue
    else
        recPVS alpha beta color depth state acc tTable p

let pvs (targetLimit: searchLimit) (s: ICoreState) (acc: accumulator) (pv: int list) =
    let transpositionTable = transpositionTable(acc.logConfig, acc)
    let result = recPVS -Int32.MaxValue Int32.MaxValue (startColor s) targetLimit s acc transpositionTable pv
    acc.successfulHashMapLookups <- acc.successfulHashMapLookups + (transpositionTable :> ITranspositionTable).successfulLookups
        
    result