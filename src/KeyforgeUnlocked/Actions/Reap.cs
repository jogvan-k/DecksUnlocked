using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public class Reap : UseCreature
  {
    public Reap(Creature creature) : base(creature)
    {
    }

    internal override void DoActionNoResolve(MutableState state)
    {
      state.Effects.Push(new Effects.Reap(Creature));
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
      return 11 * Creature.GetHashCode();
    }
  }
}