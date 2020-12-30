using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  /// <summary>
  /// Actions inherited from this class are available only when no unresolved effects are present
  /// </summary>
  public abstract class BasicAction<T> : Action<T> where T : BasicAction<T>
  {
    protected BasicAction(ImmutableState origin) : base(origin)
    {
    }

    internal sealed override void DoActionNoResolve(IMutableState state)
    {
      state.HistoricData.ActionPlayedThisTurn = true;
      DoSpecificActionNoResolve(state);
    }

    protected abstract void DoSpecificActionNoResolve(IMutableState state);

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