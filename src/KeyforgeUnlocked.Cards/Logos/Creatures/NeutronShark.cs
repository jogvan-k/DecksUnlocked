using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Logos.Creatures
{
  public class NeutronShark : CreatureCard
  {
    const int Power = 1;
    const int Armor = 0;

    static readonly Trait[] Traits =
    {
      Trait.Beast, Trait.Mutant
    };

    static readonly Callback Effect = (s, _, _) =>
    {
      s.Effects.Push(new TargetSingle((s, t, _) => s.Destroy(t), TargetType.Creature | TargetType.Artifact, Target.Own));
      s.Effects.Push(new TargetSingle((s, t, _) => s.Destroy(t), TargetType.Creature | TargetType.Artifact, Target.Opponens));
    };
    
    static readonly StatePredicate RepeatIf = s =>
    {
      var c = s.DiscardTopOfDeck();
      return c != null && c.House != House.Logos;
    };

    static readonly Callback PlayFightReapAbility = (s, _, _) =>
    { 
      s.AddEffect(new RepeatableEffect(Effect, RepeatIf, true));
    };

    public NeutronShark() : this(House.Logos)
    {
    }

    public NeutronShark(House house) : base(house, Power, Armor, Traits, playAbility: PlayFightReapAbility, fightAbility: PlayFightReapAbility, reapAbility: PlayFightReapAbility)
    {
    }
  }
}