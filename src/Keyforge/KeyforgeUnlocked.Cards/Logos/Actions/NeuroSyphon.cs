using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Logos.Actions
{
  [CardInfo("Neuro Syphon", Rarity.Uncommon,
    "Play: If your opponent has more æmber than you, steal 1 æmber and draw a card.")]
  [ExpansionSet(Expansion.CotA, 116)]
  [ExpansionSet(Expansion.MM, 92)]
  public sealed class NeuroSyphon : ActionCard
  {
    static readonly Pip[] Pips = {Pip.Aember};

    static readonly Callback PlayAbility =
      (s, _, p) =>
      {
        if (s.Aember[p.Other()] > s.Aember[p])
        {
          s.StealAember(p);
          s.Draw(p);
        }
      };

    public NeuroSyphon() : this(House.Logos)
    {
    }

    public NeuroSyphon(House house) : base(house, pips: Pips, PlayAbility)
    {
    }
  }
}