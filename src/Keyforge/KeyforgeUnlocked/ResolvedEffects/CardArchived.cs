using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public class CardArchived : ResolvedEffectWithIdentifiable<CardArchived>
  {
    public CardArchived(IIdentifiable id) : base(id)
    {
    }

    public override string ToString()
    {
      return "Card archived";
    }
  }
}