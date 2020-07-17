using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public abstract class UseCreature : IEffect
  {
    public readonly string CreatureId;
    protected MutableCreature Creature;

    public UseCreature(string creatureId)
    {
      CreatureId = creatureId;
    }

    public void Resolve(MutableState state)
    {
      CreatureUtil.FindAndValidateCreatureReady(state, CreatureId, out var creature);
      Creature = creature.ToMutable();
      Creature.IsReady = false;
      SpecificResolve(state);
      CreatureUtil.SetCreature(state, Creature.ToImmutable());
    }

    protected abstract void SpecificResolve(MutableState state);
  }
}