using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public abstract class UseCreature : BasicAction
  {
    public string CreatureId;

    public UseCreature(string creatureId)
    {
      CreatureId = creatureId;
    }

    internal override void Validate(IState state)
    {
      base.Validate(state);
      CreatureUtil.FindAndValidateCreatureReady(state, CreatureId, out _);
    }
  }
}