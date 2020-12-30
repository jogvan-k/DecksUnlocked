using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Actions
{
  public sealed class Punch : ActionCard
  {
    static readonly Pip[] Pips = {Pip.Aember};

    static readonly Callback PlayAbility =
      (s, _, _) => s.AddEffect(new TargetSingleCreature((s, t, _) => s.DamageCreature(t, 3)));

    public Punch() : this(House.Brobnar)
    {
    }

    public Punch(House house) : base(house, pips: Pips, PlayAbility)
    {
    }
  }
}