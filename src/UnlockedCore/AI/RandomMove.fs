﻿module ClassLibrary1.AI.RandomMove

open System
open UnlockedCore
open Algorithms.RandomMove

type RandomMoveAI() =
    let rng = Random()

    interface IGameAI with
        member this.DetermineAction s =
            randomMoveAI rng s |> Array.singleton
