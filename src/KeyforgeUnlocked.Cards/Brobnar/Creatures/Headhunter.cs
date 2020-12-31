﻿using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Creatures
{
  public class Headhunter : CreatureCard
  {
    const int Power = 5;
    const int Armor = 0;
    static readonly Trait[] Traits = {Trait.Giant};
    static readonly Callback FightAbility = (s, _, _) => s.GainAember();

    public Headhunter() : this(House.Brobnar)
    {
    }

    public Headhunter(House house) : base(house, Power, Armor, Traits, fightAbility: FightAbility)
    {
    }
  }
}