using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public class InitiateGame : EffectBase<InitiateGame>
  {
    protected override void ResolveImpl(MutableState state)
    {
      state.Effects.Enqueue(new DrawInitialHands());
      state.Effects.Enqueue(new DeclareHouse());
      state.Effects.Enqueue(new FirstTurn());
      state.Effects.Enqueue(new ReadyCardsAndRestoreArmor());
      state.Effects.Enqueue(new EndTurn());
    }
  }
}