using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public class Reap : UseCreature<Reap>
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
  }
}