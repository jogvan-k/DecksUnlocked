using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects
{
  public class Reap : UseCreature<Reap>
  {
    public Reap(Creature creature) : base(creature)
    {
    }

    protected override void SpecificResolve(IMutableState state, Creature creature)
    {
      state.Aember[state.PlayerTurn]++;
      state.ResolvedEffects.Add(new Reaped(creature));
      creature.Card.CardReapAbility?.Invoke(state, creature, state.PlayerTurn);
    }
  }
}