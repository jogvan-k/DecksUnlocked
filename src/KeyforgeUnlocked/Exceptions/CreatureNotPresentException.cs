using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
  public class CreatureNotPresentException : KeyforgeUnlockedException
  {
    public string CreatureId;
    public CreatureNotPresentException(IState state, string creatureId) : base(state)
    {
      CreatureId = creatureId;
    }
  }
}