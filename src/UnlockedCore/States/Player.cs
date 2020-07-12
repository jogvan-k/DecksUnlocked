namespace UnlockedCore.States
{
  public enum Player
  {
    Player1,
    Player2
  }

  public static class PlayerExtensions
  {
    public static Player Other(this Player player)
    {
      return player == Player.Player1 ? Player.Player2 : Player.Player1;
    }
  }
}