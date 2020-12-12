using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  /// <summary>
  /// Actions inherited from this class are available only when no unresolved effects are present
  /// </summary>
  public abstract class BasicAction : Action<BasicAction>
  {
    protected BasicAction(ImmutableState origin) : base(origin)
    {
    }

    internal override void Validate(IState state)
    {
      ValidateNoUnresolvedEffects(state);
    }

    static void ValidateNoUnresolvedEffects(IState state)
    {
      if (state.Effects.Length != 0 && state.TurnNumber > 1)
      {
        throw new UnresolvedEffectsException(state);
      }
    }
  }
}