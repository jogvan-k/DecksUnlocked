using KeyforgeUnlocked.Effects;

namespace KeyforgeUnlocked.States.Extensions
{
  public static class EffectsMutableStateExtensions
  {
    public static void AddEffect(
      this MutableState state,
      IEffect effect)
    {
      state.Effects.Push(effect);
    }
  }
}