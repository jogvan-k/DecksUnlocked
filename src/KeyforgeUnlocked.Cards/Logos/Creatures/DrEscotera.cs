using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Logos.Creatures
{
  [CardName("Dr. Escotera")]
  public class DrEscotera : CreatureCard
  {
    const int Power = 4;
    const int Armor = 0;
    static readonly Trait[] Traits = {Trait.Cyborg, Trait.Scientist};
    static readonly Callback PlayAbility = (s, _, p) =>
    {
      var opponentKeys = s.Keys[p.Other()];
      s.GainAember(p, opponentKeys);
    };

    public DrEscotera() : this(House.Logos)
    {
    }

    public DrEscotera(House house) : base(house, Power, Armor, Traits, playAbility: PlayAbility)
    {
      
    }
  }
}