using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.CreatureCards;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Logos.Creatures
{
  public class DrEscotera : CreatureCard
  {
    const int power = 4;
    const int armor = 0;
    static CreatureType[] creatureTypes = {CreatureType.Cyborg, CreatureType.Scientist};
    static Callback playAbility = (s, id) =>
    {
      var player = s.playerTurn;
      var opponentKeys = s.Keys[player.Other()];
      s.GainAember(player, opponentKeys);
    };
    
    public static string SpecialName = "Dr. Escotera";

    public DrEscotera() : this(House.Logos)
    {
    }

    public DrEscotera(House house) : base(house, power, armor, creatureTypes, playAbility: playAbility)
    {
      
    }
  }
}