using System;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;

namespace KeyforgeUnlocked.Effects
{
  public abstract class UseCreature<T> : EffectWithIdentifiable<T> where T : UseCreature<T>
  {
    public UseCreature(Creature creature) : base(creature)
    {
    }

    protected override void ResolveImpl(IMutableState state)
    {
      var creature = state.FindCreature(Id, out _, out _);
      if(!creature.IsReady)
        throw new CreatureNotReadyException(state, creature);
      creature.IsReady = false;
      state.SetCreature(creature);
      
      SpecificResolve(state, creature);
    }

    protected abstract void SpecificResolve(IMutableState state, Creature creature);
  }
}