module UnlockedCore.Algorithms.TranspositionTable

open UnlockedCore
open UnlockedCore.Algorithms.Accumulator

type ITranspositionTable =
    abstract add: int -> (int *int list) -> unit
    abstract lookup: int -> (int * int list) Option
    abstract successfulLookups: int with get, set


let logSuccessfulHashMapLookup (logConfig: LoggingConfiguration) = logConfig.HasFlag(LoggingConfiguration.LogSuccessfulHashMapLookup)
let avoidHashMap (searchConfig: SearchConfiguration) = searchConfig.HasFlag(SearchConfiguration.NoHashTable)

type transpositionTable(logConfig: LoggingConfiguration, ?acc: accumulator) =
    let searchConfig = match acc with
                       | Some(x) -> x.searchConfig
                       | _ -> SearchConfiguration.NoRestrictions
    let mutable _lookups = 0
    let _lookupIncrement = fun i -> _lookups <- _lookups + i
                                
    let mutable _hashMap = Map.empty
    let _lookupHashMap: int -> (int * int list) option =
        if(logSuccessfulHashMapLookup logConfig)
        then fun hash -> let lookup = _hashMap.TryFind(hash)
                         if(lookup.IsSome)
                         then _lookupIncrement 1
                         lookup
        else fun hash -> _hashMap.TryFind(hash)
    let _addToHashMap =
        if(avoidHashMap searchConfig)
        then fun (_, _) -> ()
        else fun (hash, value) -> _hashMap <- _hashMap.Add(hash, value)

    interface ITranspositionTable with
        member this.add hash value = _addToHashMap(hash, value)
        member this.lookup hash = _lookupHashMap hash
        member this.successfulLookups
            with get() = _lookups
            and set(value) = _lookups <-  value
    
type noTranspositionTable() =
    interface ITranspositionTable with
        member this.add _ _ = ()
        member this.lookup _ = None
        member this.successfulLookups
            with get() = 0
            and set(_) = ()