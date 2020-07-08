using KeyforgeUnlocked.States;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Effects
{
  public class ChangePlayer : Effect
  {
    public ChangePlayer(Player player) : base(player)
    {
    }

    public override void Resolve(MutableState state)
    {
      state.PlayerTurn = state.PlayerTurn.Other();
      state.TurnNumber++;
    }
  }
}