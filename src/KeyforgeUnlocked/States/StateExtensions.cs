using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public static class StateExtensions
  {
    public static bool Draw(this MutableState state,
      Player player)
    {
      if (state.Decks[player].TryPop(out var card))
      {
        state.Hands[player].Add(card);
        return true;
      }

      return false;
    }
  }
}