using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Logos.Actions
{
  public sealed class NeuroSyphon : ActionCard
  {
    static readonly Pip[] Pips = {Pip.Aember};

    static readonly Callback PlayAbility =
      (s, _) =>
      {
        if (s.Aember[s.playerTurn.Other()] > s.Aember[s.playerTurn])
        {
          s.StealAember(s.playerTurn);
          s.Draw(s.playerTurn);
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