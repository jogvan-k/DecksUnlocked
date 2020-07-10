using KeyforgeUnlocked.States;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Effects
{
  public interface IEffect
  {
    public abstract void Resolve(MutableState state);
  }
}