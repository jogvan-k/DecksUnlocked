﻿using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Untamed.Creatures
{
  [CardName("Lupo the Scarred")]
  public class LupoTheScarred : CreatureCard
  {
    const int Power = 6;
    const int Armor = 0;

    static readonly Trait[] Traits =
    {
      Trait.Beast
    };

    static readonly Keyword[] Keywords =
    {
      Keyword.Skirmish
    };

    static readonly Callback PlayAbility = (s, _, _) =>
    {
      var effect = new TargetSingleCreature(
        (s, t, _) => s.DamageCreature(t, 2), Targets.Opponens);
      s.AddEffect(effect);
    };

    public LupoTheScarred() : this(House.Untamed)
    {
    }

    public LupoTheScarred(House house) : base(house, Power, Armor, Traits, keywords: Keywords, playAbility: PlayAbility)
    {
    }
  }
}