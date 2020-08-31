namespace ClassLibrary1.AITypes

open System
open UnlockedCore

type MinimaxAI(evaluator : IEvaluator, depth) =
    interface IGameAI with
        member this.DetermineAction s = AIMethods.minimaxAI evaluator depth s |> snd |> List.toArray

type RandomMoveAI() =
    let rng = Random()
    interface IGameAI with
        member this.DetermineAction s = AIMethods.randomMoveAI rng s |> Array.singleton