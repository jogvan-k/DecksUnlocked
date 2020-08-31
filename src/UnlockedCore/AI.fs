module AIMethods
open System
open UnlockedCore

let (|Node|Terminal|) (s : ICoreState) =
    let actions = s.Actions()
    if(actions.Length = 0)
        then Terminal
        else Node (Array.map (fun (a : ICoreAction) -> a.DoCoreAction()) actions)

let rec minimaxAI (evaluator : IEvaluator) (depth : int) (s : ICoreState) =
   if(depth <= 0) then (evaluator.Evaluate(s), [])
   else
   match s with
   | Terminal -> (evaluator.Evaluate(s), [])
   | Node a ->
       let evaluatedStates = a |> Array.mapi (fun i s ->
           let eval = (minimaxAI evaluator (depth - 1) s)
           (fst eval, i :: (snd eval)))
       
       if(s.PlayerTurn = Player.Player1)
           then evaluatedStates |> Array.maxBy (fun r -> fst r)
           else evaluatedStates |> Array.minBy (fun r -> fst r)

let randomMoveAI (rng : Random) (s :ICoreState) =
            let actions = s.Actions()
            rng.Next() % actions.Length