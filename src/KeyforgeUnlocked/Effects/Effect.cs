using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public abstract class Effect
  {
    public abstract void Resolve(MutableState state);
  }
}