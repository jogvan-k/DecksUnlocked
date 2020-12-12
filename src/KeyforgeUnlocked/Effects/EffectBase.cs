using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public abstract class EffectBase<T> : IEffect where T : EffectBase<T>
  {

    protected virtual bool Equals(T other)
    {
      return true;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((T) obj);
    }

    public override int GetHashCode()
    {
      return GetType().GetHashCode();
    }

    public void Resolve(MutableState state)
    {
      ResolveImpl(state);
    }

    protected abstract void ResolveImpl(MutableState state);
  }
}