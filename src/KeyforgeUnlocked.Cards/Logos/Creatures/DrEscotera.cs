using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Logos.Creatures
{
  [CardName("Dr. Escotera")]
  public class DrEscotera : CreatureCard
  {
    const int Power = 4;
    const int Armor = 0;
    static readonly CreatureType[] CreatureTypes = {CreatureType.Cyborg, CreatureType.Scientist};
    static readonly Callback PlayAbility = (s, _) =>
    {
      var player = s.playerTurn;
      var opponentKeys = s.Keys[player.Other()];
      s.GainAember(player, opponentKeys);
    };

    public DrEscotera() : this(House.Logos)
    {
    }

    public DrEscotera(House house) : base(house, Power, Armor, CreatureTypes, playAbility: PlayAbility)
    {
      
    }
  }
}