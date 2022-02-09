using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class EndTurn : BasicAction<EndTurn>
  {
    public EndTurn(ImmutableState originState) : base(originState)
    {
    }

    protected override void DoSpecificActionNoResolve(IMutableState state)
    {
      state.Effects.Enqueue(new ReadyCardsAndRestoreArmor());
      state.Effects.Enqueue(new DrawToHandLimit());
      state.Effects.Enqueue(new Effects.EndTurn());
    }

    public override string ToString()
    {
      return "End turn";
    }
  }
}