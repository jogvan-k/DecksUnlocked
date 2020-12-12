module UnlockedCore.Algorithms.RandomMove

open System
open UnlockedCore

let randomMoveAI (rng: Random) (s: ICoreState) =
    let actions = s.Actions()
    rng.Next() % actions.Length
