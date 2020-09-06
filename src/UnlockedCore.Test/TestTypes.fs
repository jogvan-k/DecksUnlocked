module UnlockedCore.TestTypes

open UnlockedCore

let p1 = Player.Player1
let p2 = Player.Player2
type node(playerTurn, turnNumber, value, hash, nodes : node[]) =
    new(playerTurn, turnNumber, value, hash, singleNode) = node(playerTurn, turnNumber, value, hash, [|singleNode|])
    new(playerTurn, turnNumber, value, hash) = node (playerTurn, turnNumber, value, hash, Array.empty)
    member this.playerTurn = playerTurn
    member this.turnNumber = turnNumber
    member this.value = value
    member this.nodes = nodes
    override this.GetHashCode () = hash
    override this.Equals other = hash = other.GetHashCode()

    interface ICoreState with
        member this.PlayerTurn = playerTurn
        member this.TurnNumber = turnNumber

        member this.Actions() =
            Array.map (fun n -> action (n) :> ICoreAction) nodes

and action(node) =
    interface ICoreAction with
        member this.Origin =
            new node(Player.Player1, 0, 0, 0) :> ICoreState

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
    let (player, turnNo, value, hash) = evalFun counter
    let (d, h) = counter
    if(d = n)
    then node(player, turnNo, value, hash)
    else
        //let currentHeight = 
        let nodes = [|0..1..b-1|] |> Array.map (fun i -> recComplexTree evalFun (d + 1, b * h + i) n b)
        node(player, turnNo, value, hash, nodes)
let complexTree evalFun n b =
    let counter = (0, 0)
    recComplexTree evalFun counter n b
    
let rec invertTree (t : node) =
        let otherPlayer = if(t.playerTurn = p1) then p2 else p1
        if(t.nodes.Length = 0)
        then
            node(otherPlayer, t.turnNumber, t.value, t.GetHashCode())
        else
            let nodes = t.nodes |> Array.map invertTree
            node(otherPlayer, t.turnNumber, t.value, t.GetHashCode(), nodes)