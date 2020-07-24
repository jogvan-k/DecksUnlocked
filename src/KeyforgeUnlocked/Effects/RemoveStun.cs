using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class RemoveStun : IEffect
  {
    public Creature Creature { get; }

    public RemoveStun(Creature creature)
    {
      Creature = creature;
    }

    public void Resolve(MutableState state)
    {
      throw new System.NotImplementedException();
    }

    bool Equals(RemoveStun other)
    {
      return Creature.Equals(other.Creature);
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is RemoveStun other && Equals(other);
    }

    public override int GetHashCode()
    {
      return Creature.GetHashCode();
    }
  }
}