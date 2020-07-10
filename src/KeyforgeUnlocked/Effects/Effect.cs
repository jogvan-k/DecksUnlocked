using KeyforgeUnlocked.States;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Effects
{
  public abstract class Effect
  {
    protected Effect()
    {
    }

    public abstract void Resolve(MutableState state);
  }
}