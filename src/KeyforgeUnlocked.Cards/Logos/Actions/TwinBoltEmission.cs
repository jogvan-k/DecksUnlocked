using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Logos.Actions
{
  public sealed class TwinBoltEmission : ActionCard
  {
    static readonly Pip[] Pips = {Pip.Aember};

    static readonly Callback PlayAbility =
      (s, _) => s.AddEffect(
        new TargetSingleCreature(
          (s, t) =>
          {
            s.DamageCreature(t, 2);
            s.AddEffect(new TargetSingleCreature((s2, t2) => s2.DamageCreature(t2, 2), Delegates.AllExcept(t)));
          }, Delegates.All));

    public TwinBoltEmission() : this(House.Logos)
    {
    }

    public TwinBoltEmission(House house) : base(house, pips: Pips, PlayAbility)
    {
    }
  }
}