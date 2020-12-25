using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Actions
{
  public sealed class Punch : ActionCard
  {
    static readonly Pip[] Pips = {Pip.Aember};

    static readonly Callback PlayAbility =
      (s, _) => s.AddEffect(new TargetSingleCreature((s, t) => s.DamageCreature(t, 3), Delegates.All));

    public Punch() : this(House.Brobnar)
    {
    }

    public Punch(House house) : base(house, pips: Pips, PlayAbility)
    {
    }
  }
}