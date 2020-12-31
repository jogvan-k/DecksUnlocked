using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Logos.Actions
{
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