module UnlockedCore.Algorithms.Accumulator

open UnlockedCore

let logPrunedPaths (logConfig: LoggingConfiguration) = logConfig.HasFlag(LoggingConfiguration.LogPrunedPaths)
let avoidHashMap (searchConfig: SearchConfiguration) = searchConfig.HasFlag(SearchConfiguration.NoHashTable)
let logSuccessfulHashMapLookup (logConfig: LoggingConfiguration) = logConfig.HasFlag(LoggingConfiguration.LogSuccessfulHashMapLookup)

type accumulator(evaluator : ICoreState -> int, logConfig: LoggingConfiguration, ?searchConfig0: SearchConfiguration) =
    let searchConfig = defaultArg searchConfig0 SearchConfiguration.NoRestrictions
    let mutable _prunedPaths = 0
    let mutable _successfulHashMapLookups = 0
    let mutable _hashMap = Map.empty
    let _lookupHashMap: int -> (int * int list) option =
        if(logSuccessfulHashMapLookup logConfig)
        then fun hash -> let lookup = _hashMap.TryFind(hash)
                         if(lookup.IsSome)
                         then _successfulHashMapLookups <- _successfulHashMapLookups + 1
                         lookup
        else fun hash -> _hashMap.TryFind(hash)
        
    let _incrementPrunedPaths =
        if(logPrunedPaths logConfig)
        then fun () -> _prunedPaths <- _prunedPaths + 1
        else fun () -> ()
        
    let _addToHashMap =
        if(avoidHashMap searchConfig)
        then fun (_, _) -> ()
        else fun (hash, value) -> _hashMap <- _hashMap.Add(hash, value)
    member this.incrementPrunedPaths () = _incrementPrunedPaths ()
    member this.logConfig = logConfig
    member this.eval = evaluator
    member this.addToHashMap hash value = _addToHashMap(hash, value)
    member this.lookupHashMap hash = _lookupHashMap hash
    member this.prunedPaths
        with get() = _prunedPaths
    member this.successfulHashMapLookups
        with get() = _successfulHashMapLookups