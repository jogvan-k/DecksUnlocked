module UnlockedCore.MCTS.MonteCarloTreeSearch

open UnlockedCore
open UnlockedCore.MCTS.Algorithm

type MonteCarloTreeSearch =
    interface IGameAI with
        member this.DetermineAction(state) =
            search state