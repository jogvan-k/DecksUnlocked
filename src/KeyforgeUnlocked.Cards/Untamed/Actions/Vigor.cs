using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Untamed.Actions
{
  public sealed class Vigor : ActionCard
  {
    static readonly Pip[] Pips = {Pip.Aember};

    static readonly Callback PlayAbility =
      (s, _) => s.AddEffect(new TargetSingleCreature(
        (s, t) =>
        {
          var healedAmount = s.HealCreature(t, 3);
          if(healedAmount == 3) s.GainAember(s.playerTurn);
        }));

    public Vigor() : this(House.Untamed)
    {
    }

    public Vigor(House house) : base(house, pips: Pips, PlayAbility)
    {
    }
  }
}