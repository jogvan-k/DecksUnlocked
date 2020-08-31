module UnlockedCore.TestTypes

open UnlockedCore

type node(playerTurn, turnNumber, value, nodes : node[]) =
    new(playerTurn, turnNumber, value, singleNode) = node(playerTurn, turnNumber, value, [|singleNode|])
    new(playerTurn, turnNumber, value) = node (playerTurn, turnNumber, value, Array.empty)
    member this.playerTurn = playerTurn
    member this.turnNumber = turnNumber
    member this.value = value
    member this.nodes = nodes

    interface ICoreState with
        member this.PlayerTurn = playerTurn
        member this.TurnNumber = turnNumber

        member this.Actions() =
            Array.map (fun n -> action (n) :> ICoreAction) nodes

and action(node) =
    interface ICoreAction with
        member this.Origin =
            new node(Player.Player1, 0, 0) :> ICoreState

        member this.DoCoreAction() = node :> ICoreState

type evaluator() =
    interface IEvaluator with
        member this.Evaluate s =
            (s :?> node).value 