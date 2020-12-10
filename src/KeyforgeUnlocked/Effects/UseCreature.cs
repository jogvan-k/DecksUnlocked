using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public abstract class UseCreature<T> : EffectBase<T> where T : UseCreature<T>
  {
    public readonly Creature Creature;

    public UseCreature(Creature creature)
    {
      Creature = creature;
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

    protected override bool Equals(T other)
    {
      return Creature.Equals(other.Creature);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode() * Constants.PrimeHashBase + Creature.GetHashCode();
    }
  }
}