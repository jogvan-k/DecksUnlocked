using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  /// <summary>
  /// Actions inherited from this class are available only when no unresolved effects are present
  /// </summary>
  public abstract class BasicAction : Action
  {
    protected static void Validate(IState state)
    {
      ValidateNoUnresolvedEffects(state);
    }

    static void ValidateNoUnresolvedEffects(IState state)
    {
      if (state.Effects.Count != 0)
      {
        throw new UnresolvedEffectsException(state);
      }
    }
  }
}