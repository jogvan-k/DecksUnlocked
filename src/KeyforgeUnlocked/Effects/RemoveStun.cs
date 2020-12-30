using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;

namespace KeyforgeUnlocked.Effects
{
  public sealed class RemoveStun : UseCreature<RemoveStun>
  {
    public RemoveStun(Creature creature) : base(creature)
    {
    }

    protected override void SpecificResolve(IMutableState state, Creature creature)
    {
      creature.State = creature.State & ~CreatureState.Stunned;
      state.SetCreature(creature);
      state.ResolvedEffects.Add(new StunRemoved(creature));
    }
  }
}