using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public abstract class UseCreature : IEffect
  {
    public readonly Creature Creature;

    public UseCreature(Creature creature)
    {
      Creature = creature;
    }

    public void Resolve(MutableState state)
    {
      if(!Creature.IsReady)
        throw new CreatureNotReadyException(state, Creature);
      SpecificResolve(state);
      var creature = Creature;
      creature.IsReady = false;
      state.SetCreature(creature);
    }

    protected abstract void SpecificResolve(MutableState state);
  }
}