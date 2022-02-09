using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects
{
  public abstract class EffectBase<T> : Equatable<T>, IEffect
  {
    public void Resolve(IMutableState state)
    {
      ResolveImpl(state);
    }

    protected abstract void ResolveImpl(IMutableState state);
  }
}