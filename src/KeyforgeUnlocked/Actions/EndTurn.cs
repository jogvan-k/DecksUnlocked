using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class EndTurn : BasicAction
  {
    public EndTurn(ImmutableState origin) : base(origin)
    {
    }

    protected override void DoSpecificActionNoResolve(MutableState state)
    {
      state.Effects.Enqueue(new ReadyCardsAndRestoreArmor());
      state.Effects.Enqueue(new DrawToHandLimit());
      state.Effects.Enqueue(new Effects.EndTurn());
    }
  }
}