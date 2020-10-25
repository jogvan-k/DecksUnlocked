using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class RemoveStun : EffectBase<RemoveStun>
  {
    public Creature Creature { get; }

    public RemoveStun(Creature creature)
    {
      Creature = creature;
    }

    protected override void ResolveImpl(MutableState state)
    {
      var creature = Creature;
      creature.State = creature.State & ~CreatureState.Stunned;
      creature.IsReady = false;
      state.SetCreature(creature);
      state.ResolvedEffects.Add(new StunRemoved(creature));
    }

    protected override bool Equals(RemoveStun other)
    {
      return base.Equals(other);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode() * Constants.PrimeHashBase + Creature.GetHashCode();
    }
  }
}