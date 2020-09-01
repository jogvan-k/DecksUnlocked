using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public class Reap : UseCreature
  {
    public Reap(Creature creature) : base(creature)
    {
    }

    protected override void SpecificResolve(MutableState state, Creature creature)
    {
      state.Aember[state.PlayerTurn]++;
      state.ResolvedEffects.Add(new Reaped(creature));
      creature.Card.ReapAbility?.Invoke(state, creature.Id);
    }

    protected bool Equals(Reap other)
    {
      return Creature.Equals(other.Creature);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Reap) obj);
    }

    public override int GetHashCode()
    {
      return Creature.GetHashCode();
    }
  }
}