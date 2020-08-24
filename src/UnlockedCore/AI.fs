namespace UnlockedCore.AI

open System
open UnlockedCore

type RandomMoveAI() =
    let rng = new Random()
    interface IGameAI with
        member this.DetermineAction s =
            let actions = s.Actions()
            let randomActionIndex = rng.Next() % actions.Length
            actions.[randomActionIndex]