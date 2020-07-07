using KeyforgeUnlocked.States;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Effects
{
  public abstract class Effect
  {
    protected readonly Player Player;

    protected Effect(Player player)
    {
      Player = player;
    }

    public abstract void Resolve(MutableState state);
  }
}