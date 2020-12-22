using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Actions
{
  [CardName("Burn the Stockpile")]
  public sealed class BurnTheStockpile : ActionCard
  {
    static readonly Callback PlayAbility = (s, _) =>
    {
      if (s.Aember[s.playerTurn.Other()] >= 7) s.LoseAember(s.playerTurn.Other(), 4);
    };

    public BurnTheStockpile() : this(House.Brobnar)
    {
    }

    public BurnTheStockpile(House house) : base(house, PlayAbility)
    {
    }
  }
}