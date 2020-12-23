using System;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;

namespace KeyforgeUnlocked.Effects
{
  public abstract class UseCreature<T> : EffectWithCreature<T> where T : UseCreature<T>
  {
    public UseCreature(Creature creature) : base(creature)
    {
    }

    protected override void ResolveImpl(MutableState state)
    {
      if(!Creature.IsReady)
        throw new CreatureNotReadyException(state, Creature);
      var creature = state.FindCreature(Creature.Id, out _, out _);
      creature.IsReady = false;
      state.SetCreature(creature);
      
      SpecificResolve(state, creature);
    }

    protected abstract void SpecificResolve(MutableState state, Creature creature);
  }
}