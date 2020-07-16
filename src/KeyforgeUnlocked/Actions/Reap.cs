using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public class Reap : UseCreature
  {
    public Reap(string creatureId) : base(creatureId)
    {
    }

    internal override void DoActionNoResolve(MutableState state)
    {
      state.Effects.Push(new Effects.Reap(CreatureId));
    }

    protected bool Equals(Reap other)
    {
      return CreatureId.Equals(other.CreatureId);
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
      return CreatureId.GetHashCode();
    }
  }
}