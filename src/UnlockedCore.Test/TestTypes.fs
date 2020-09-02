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
            
let evaluator = evaluator()
let evaluatorFunc (s : ICoreState) = (s :?> node).value
// n: depth number
// b: new branches after each depth
// counter: tuple of given depth * given height
let rec recComplexTree evalFun counter n b =
    let (player, turnNo, value) = evalFun counter
    let (d, h) = counter
    if(d = n)
    then node(player, turnNo, value)
    else
        //let currentHeight = 
        let nodes = [|0..1..b-1|] |> Array.map (fun i -> recComplexTree evalFun (d + 1, b * h + i) n b)
        node(player, turnNo, value, nodes)
and complexTree evalFun n b =
    let counter = (0, 0)
    recComplexTree evalFun counter n b