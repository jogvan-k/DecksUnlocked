using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Cards.ActionCards.Brobnar
{
  public sealed class BurnTheStockpile : ActionCard
  {
    static readonly Callback _playAbility = (s, i) => {if(s.Aember[s.playerTurn.Other()] >= 7) s.LoseAember(s.playerTurn.Other(), 4);};
    public static string SpecialName = "Burn the Stockpile";

    public BurnTheStockpile() : this(House.Brobnar)
    {
    }

    public BurnTheStockpile(House house) : base(house, _playAbility)
    {
    }
  }
}