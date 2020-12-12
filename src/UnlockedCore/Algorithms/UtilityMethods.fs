module UnlockedCore.Algorithms.UtilityMethods

open UnlockedCore

let searchDepthLimit = 100

let changingPlayer (s: ICoreState) =
    s.PreviousState.IsSome
    && s.PlayerTurn
    <> s.PreviousState.Value.PlayerTurn

let reduceSearchLimit =
    function
    | Until (turn, depth) ->
        if (depth >= searchDepthLimit)
        then failwith (sprintf "search depth exceeded %i" searchDepthLimit)
        else Until(turn, depth + 1)
    | Depth remaining -> Depth(remaining - 1)

let increaseSearchLimit =
    function
    | Until (turn, _) -> Until(turn + 1, 0)
    | Depth remaining -> Depth(remaining + 1)

let nextCandidate f alpha beta color (state: ICoreState) depth =
    let nextDepth = reduceSearchLimit depth
    if (changingPlayer state) then
        fun acc ->
            let (fs, sn) =
                f (-1 * beta) (-1 * alpha) (-color) nextDepth state acc

            (-fs, sn)
    else
        f alpha beta color depth state

let startColor =
    fun (s: ICoreState) -> if (s.PlayerTurn = Player.Player1) then 1 else -1
