using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Actions
{
  [CardName("Burn the Stockpile")]
  public sealed class BurnTheStockpile : ActionCard
  {
    static readonly Callback PlayAbility = (s, _, p) =>
    {
      if (s.Aember[p.Other()] >= 7) s.LoseAember(p.Other(), 4);
    };

    public BurnTheStockpile() : this(House.Brobnar)
    {
    }

    public BurnTheStockpile(House house) : base(house, playAbility: PlayAbility)
    {
    }
  }
}