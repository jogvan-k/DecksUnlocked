using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects
{
  public sealed class CreatureAbility : UseCreature<CreatureAbility>
  {
    public CreatureAbility(Creature creature) : base(creature)
    {
    }

    protected override void SpecificResolve(MutableState state, Creature creature)
    {
      creature.Card.CardCreatureAbility(state, creature);
    }
  }
}