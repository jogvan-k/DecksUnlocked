using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Logos.Actions
{
  public sealed class Labwork : ActionCard
  {
    static readonly Pip[] Pips = {Pip.Aember};

    static readonly Callback PlayAbility =
      (s, _, _) => s.AddEffect(new TargetSingleCardInHand((s, t, _) => s.ArchiveFromHand(t), Target.Own));

    public Labwork() : this(House.Logos)
    {
    }

    public Labwork(House house) : base(house, pips: Pips, PlayAbility)
    {
    }
  }
}