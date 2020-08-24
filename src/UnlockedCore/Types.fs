namespace UnlockedCore

type Player =
    Player1 = 1 | Player2 = 2
    
type ICoreState =
    abstract member PlayerTurn : Player
    abstract member TurnNumber : int
    abstract member IsGameOver : bool
    abstract member Actions : unit -> ICoreAction[]
    
and ICoreAction =
    abstract member DoCoreAction : ICoreState -> ICoreState
    
type IGameAI =
    abstract member DetermineAction : ICoreState -> ICoreAction