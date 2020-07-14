using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
  public class CreatureNotPresentException : KeyforgeUnlockedException
  {
    public string CreatureId;
    public CreatureNotPresentException(string creatureId, IState state) : base(state)
    {
      CreatureId = creatureId;
    }
  }
}