using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;

namespace KeyforgeUnlocked.Effects
{
  public sealed class RemoveStun : EffectWithCreature<RemoveStun>
  {
    public RemoveStun(Creature creature) : base(creature)
    {
    }

    protected override void ResolveImpl(MutableState state)
    {
      var creature = Creature;
      creature.State = creature.State & ~CreatureState.Stunned;
      creature.IsReady = false;
      state.SetCreature(creature);
      state.ResolvedEffects.Add(new StunRemoved(creature));
    }
  }
}