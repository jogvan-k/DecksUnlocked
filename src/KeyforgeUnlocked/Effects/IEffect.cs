using KeyforgeUnlocked.States;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Effects
{
  public interface IEffect
  {
    public void Resolve(MutableState state);
  }
}