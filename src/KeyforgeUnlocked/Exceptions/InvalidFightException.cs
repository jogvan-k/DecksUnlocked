using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
  public sealed class InvalidFightException : KeyforgeUnlockedException
  {
    public string FightingCreatureId { get; }
    public string TargetCreatureId { get; }

    public InvalidFightException(IState state,
      string fightingCreatureId,
      string targetCreatureId) : base(state)
    {
      FightingCreatureId = fightingCreatureId;
      TargetCreatureId = targetCreatureId;
    }
  }
}