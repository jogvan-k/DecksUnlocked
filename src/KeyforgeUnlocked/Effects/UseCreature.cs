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
      var creature = state.FindCreature(Creature.Id, out _);
      creature.IsReady = false;
      state.SetCreature(creature);
      
      SpecificResolve(state, creature);
    }

    protected abstract void SpecificResolve(MutableState state, Creature creature);
  }
}