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
    match s with
    | SearchLimit depth _
    | Terminal _ -> (color * acc.eval s, [])
    | Node (actions) ->
        let stateHash = s.GetHashCode()
        let hashMapLookup = tTable.lookup stateHash
        if(hashMapLookup.IsSome)
        then hashMapLookup.Value
        else if List.isEmpty pv
        then recNegamax alpha beta color depth s acc tTable
        else
            let (pvAction, pvHead, pvRest) = nextPvAction pv actions
            let principal = nextCandidate alpha beta color (pvAction.DoCoreAction()) depth acc tTable pvRest
            let mutable currentBest = (fst principal, pvHead :: snd principal)
            let mutable alpha' = max alpha (fst principal)
            let restActions = actions |> Seq.filter (fun e -> (snd e) <> pvHead)
            for a in restActions do
                let action, i = a
                if(alpha' < beta)
                then let mutable candidate = nextCandidate alpha' (alpha' + 1) color (action.DoCoreAction()) depth acc noTranspositionTable []
                     if(alpha' < (fst candidate) && (fst candidate) < beta)
                     then candidate <- nextCandidate (fst candidate) beta color (action.DoCoreAction()) depth acc tTable []
                     if (fst currentBest) < (fst candidate)
                     then currentBest <- (fst candidate, i :: snd candidate)
                          alpha' <- max alpha' (fst candidate)
                     
                else acc.incrementPrunedPaths()

            tTable.add stateHash currentBest
            currentBest
and nextCandidate alpha beta color (state: ICoreState) depth acc tTable p =
    let nextDepth = reduceSearchLimit depth
    if (changingPlayer state)
    then
        recPVS (-1 * beta) (-1 * alpha) (-color) nextDepth state acc tTable p |> flipValue
    else
        recPVS alpha beta color depth state acc tTable p

let pvs (targetLimit: searchLimit) (s: ICoreState) (acc: accumulator) =
    let mutable currentLimit = startSearchLimit s targetLimit
    let mutable previousResult = (-Int32.MaxValue, [])
    while(not (isDepthReached currentLimit targetLimit)) do
        currentLimit <- increaseSearchLimit currentLimit
        let tTable = new transpositionTable(acc.logConfig, acc)
        previousResult <- recPVS -Int32.MaxValue Int32.MaxValue (startColor s) currentLimit s acc tTable (snd previousResult)
        
    previousResult