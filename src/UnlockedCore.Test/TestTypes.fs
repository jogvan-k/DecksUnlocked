module UnlockedCoreTest.TestTypes

open System
open UnlockedCore

let p1 = Player.Player1
let p2 = Player.Player2
type node(playerTurn, turnNumber, value, hash, parent : ICoreState option) =
    let mutable _children = list.Empty
    new(playerTurn, turnNumber, value, hash) = node (playerTurn, turnNumber, value, hash, None)
    
    member this.parent = parent
    member this.playerTurn = playerTurn
    member this.turnNumber = turnNumber
    member this.value = value
    member this.children
        with get() = _children
        and set(value) =
            _children <- value
    override this.GetHashCode () = hash
    override this.Equals other = hash = other.GetHashCode()
    interface ICoreState with
        member this.PlayerTurn = playerTurn
        member this.TurnNumber = turnNumber
        member this.Actions() = Array.map (fun n -> action (this, n) :> ICoreAction) (Array.ofList _children)

and action(origin, node) =
    interface ICoreAction with
        member this.Origin = origin :> ICoreState
        member this.DoCoreAction() = node :> ICoreState
    interface IComparable with
        member this.CompareTo _ = 0
    override this.Equals other = node.Equals other
    override this.GetHashCode() = node.GetHashCode()

type nb(playerTurn, turnNumber, value, hash, children) =
    new(playerTurn, turnNumber, value, hash) = nb(playerTurn, turnNumber, value, hash, list.Empty)
    member this.children = children
    member this.playerTurn = playerTurn
    member this.turnNumber = turnNumber
    member this.value = value
    member this.hash = hash
    member this.addChild (c : nb) = nb(playerTurn, turnNumber, value, hash, List.append children [c])
        
    member this.addChildren (c : nb list) =
        nb(playerTurn, turnNumber, value, hash, List.append children c)
        
    member this.build(?parent) =
        let node = node(playerTurn, turnNumber, value, hash, parent)
        node.children <- children |> List.map (fun (c : nb) -> c.build(node))
        node
        
        

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
    then nb(player, turnNo, value, hash)
    else let nodes = [0..1..b-1] |> List.map (fun i -> recComplexTree evalFun (d + 1, b * h + i) n b)
         nb(player, turnNo, value, hash, nodes)
         
let complexTree (evalFun: int * int -> Player * int * int * int) (n: int) (b: int) =
    let counter = (0, 0)
    recComplexTree evalFun counter n b
    
let rec invertTree (t : nb) =
        let otherPlayer = if(t.playerTurn = p1) then p2 else p1
        if(t.children.Length = 0)
        then
            nb(otherPlayer, t.turnNumber, t.value, t.GetHashCode())
        else
            let nodes = t.children |> List.map invertTree
            nb(otherPlayer, t.turnNumber, t.value, t.GetHashCode(), nodes)
