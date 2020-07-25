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

    protected override void SpecificResolve(MutableState state)
    {
      state.Aember[state.PlayerTurn]++;
      Creature.Card.ReapAbility?.Invoke(state, Creature.Id);
      state.ResolvedEffects.Add(new Reaped(Creature));
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